using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Services;

public class NutritionistService : INutritionistService
{
    private readonly INutritionistRepository _nutritionistRepository;
    private readonly INutritionistReviewRepository _reviewRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NutritionistService(
        INutritionistRepository nutritionistRepository,
        INutritionistReviewRepository reviewRepository,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _nutritionistRepository = nutritionistRepository;
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NutritionistProfileViewModel?> GetProfileAsync(int userId)
    {
        var nutritionist = await _nutritionistRepository.GetWithCredentialsAsync(userId);
        return nutritionist is null ? null : MapToProfileViewModel(nutritionist);
    }

    public async Task UpdateProfileAsync(int userId, UpdateNutritionistProfileInputModel input)
    {
        var nutritionist = await _nutritionistRepository.GetWithCredentialsAsync(userId);
        if (nutritionist is null) return;

        nutritionist.DisplayName = input.DisplayName;
        nutritionist.ProfessionalTitle = input.ProfessionalTitle;
        nutritionist.MainSpecialty = input.MainSpecialty;
        nutritionist.YearsOfExperience = input.YearsOfExperience;
        nutritionist.About = input.About;
        nutritionist.Specialties = input.Specialties.Count > 0
            ? string.Join(',', input.Specialties)
            : null;
        nutritionist.AvailableForNewPatients = input.AvailableForNewPatients;

        nutritionist.AcceptsOnline = input.AcceptsOnline;
        nutritionist.AcceptsInPerson = input.AcceptsInPerson;
        nutritionist.PricePerSession = input.PricePerSession;
        nutritionist.SessionDurationMinutes = input.SessionDurationMinutes;
        nutritionist.OfficeAddress = input.OfficeAddress;
        nutritionist.PaymentMethods = input.PaymentMethods.Count > 0
            ? string.Join(',', input.PaymentMethods)
            : null;

        nutritionist.FullName = input.FullName;
        nutritionist.Phone = input.Phone;
        nutritionist.City = input.City;
        nutritionist.BirthDate = input.BirthDate;

        nutritionist.Credentials.Clear();
        for (var i = 0; i < input.Credentials.Count; i++)
        {
            var desc = input.Credentials[i];
            if (!string.IsNullOrWhiteSpace(desc))
                nutritionist.Credentials.Add(new NutritionistCredential
                {
                    NutritionistId = userId,
                    Description = desc.Trim(),
                    Order = i
                });
        }

        _nutritionistRepository.Update(nutritionist);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<NutritionistCardViewModel>> SearchAsync(string? query, NutritionistSpecialtyEnum? specialty, string? modality)
    {
        var nutritionists = (await _nutritionistRepository.SearchAsync(query, specialty, modality)).ToList();
        var summaries = await _reviewRepository.GetSummariesAsync(nutritionists.Select(n => n.Id));

        return nutritionists.Select(n =>
        {
            var card = MapToCardViewModel(n);
            if (summaries.TryGetValue(n.Id, out var s))
            {
                card.AverageRating = Math.Round(s.Average, 1);
                card.ReviewCount = s.Count;
            }
            return card;
        }).ToList();
    }

    public async Task<NutritionistPublicViewModel?> GetPublicProfileAsync(int nutritionistId, int? currentUserId = null)
    {
        var nutritionist = await _nutritionistRepository.GetWithCredentialsAsync(nutritionistId);
        if (nutritionist is null) return null;

        var reviews = await _reviewRepository.GetByNutritionistAsync(nutritionistId);
        var count = reviews.Count;

        var alreadyReviewed = false;
        var isClient = false;
        if (currentUserId.HasValue)
        {
            alreadyReviewed = reviews.Any(r => r.AuthorId == currentUserId.Value);
            isClient = await _userRepository.GetByIdAsync(currentUserId.Value) is Client;
        }

        return new NutritionistPublicViewModel
        {
            Id = nutritionist.Id,
            DisplayName = nutritionist.DisplayName,
            ProfessionalTitle = nutritionist.ProfessionalTitle,
            MainSpecialty = nutritionist.MainSpecialty,
            YearsOfExperience = nutritionist.YearsOfExperience,
            About = nutritionist.About,
            CrnRegistration = nutritionist.CrnRegistration,
            CrnVerified = nutritionist.CrnVerified,
            AcceptsOnline = nutritionist.AcceptsOnline,
            AcceptsInPerson = nutritionist.AcceptsInPerson,
            PricePerSession = nutritionist.PricePerSession,
            SessionDurationMinutes = nutritionist.SessionDurationMinutes,
            City = nutritionist.City,
            Specialties = ParseEnumList<NutritionistSpecialtyEnum>(nutritionist.Specialties),
            Credentials = nutritionist.Credentials
                .OrderBy(c => c.Order)
                .Select(c => c.Description)
                .ToList(),

            AverageRating = count > 0 ? Math.Round(reviews.Average(r => r.Rating), 1) : null,
            ReviewCount = count,
            Distribution = BuildDistribution(reviews),
            Reviews = reviews.Select(r => MapToReviewViewModel(r, currentUserId)).ToList(),
            AlreadyReviewed = alreadyReviewed,
            CanReview = isClient && !alreadyReviewed,
            CanMessage = isClient
        };
    }

    private static List<RatingBucketViewModel> BuildDistribution(List<NutritionistReview> reviews)
    {
        var total = reviews.Count;
        var buckets = new List<RatingBucketViewModel>();
        for (var star = 5; star >= 1; star--)
        {
            var c = reviews.Count(r => r.Rating == star);
            buckets.Add(new RatingBucketViewModel
            {
                Star = star,
                Count = c,
                Percent = total > 0 ? (int)Math.Round(c * 100.0 / total) : 0
            });
        }
        return buckets;
    }

    private static NutritionistReviewViewModel MapToReviewViewModel(NutritionistReview r, int? currentUserId)
    {
        var name = r.IsAnonymous ? "Paciente" : r.Author.FullName;
        return new NutritionistReviewViewModel
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            IsAnonymous = r.IsAnonymous,
            AuthorName = string.IsNullOrWhiteSpace(name) ? "Paciente" : name,
            AuthorInitials = Initials(r.IsAnonymous ? "Paciente" : r.Author.FullName),
            CreatedAt = r.CreatedAt,
            CanDelete = currentUserId.HasValue && r.AuthorId == currentUserId.Value
        };
    }

    private static string Initials(string name) =>
        string.IsNullOrWhiteSpace(name)
            ? "?"
            : string.Concat(name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2).Select(w => char.ToUpper(w[0])));

    private static NutritionistCardViewModel MapToCardViewModel(Nutritionist n) => new()
    {
        Id = n.Id,
        DisplayName = n.DisplayName,
        ProfessionalTitle = n.ProfessionalTitle,
        MainSpecialty = n.MainSpecialty,
        YearsOfExperience = n.YearsOfExperience,
        CrnVerified = n.CrnVerified,
        AcceptsOnline = n.AcceptsOnline,
        AcceptsInPerson = n.AcceptsInPerson,
        PricePerSession = n.PricePerSession,
        City = n.City,
        Specialties = ParseEnumList<NutritionistSpecialtyEnum>(n.Specialties),
        AvailableForNewPatients = n.AvailableForNewPatients,
    };

    private static NutritionistProfileViewModel MapToProfileViewModel(Nutritionist n) =>
        new()
        {
            Id = n.Id,
            Email = n.Email ?? string.Empty,
            DisplayName = n.DisplayName,
            ProfessionalTitle = n.ProfessionalTitle,
            MainSpecialty = n.MainSpecialty,
            YearsOfExperience = n.YearsOfExperience,
            About = n.About,
            CrnRegistration = n.CrnRegistration,
            CrnVerified = n.CrnVerified,
            Specialties = ParseEnumList<NutritionistSpecialtyEnum>(n.Specialties),
            AvailableForNewPatients = n.AvailableForNewPatients,
            AcceptsOnline = n.AcceptsOnline,
            AcceptsInPerson = n.AcceptsInPerson,
            PricePerSession = n.PricePerSession,
            SessionDurationMinutes = n.SessionDurationMinutes,
            OfficeAddress = n.OfficeAddress,
            PaymentMethods = ParseEnumList<PaymentMethodEnum>(n.PaymentMethods),
            Credentials = n.Credentials.OrderBy(c => c.Order).Select(c => c.Description).ToList(),
            FullName = n.FullName,
            Phone = n.Phone,
            City = n.City,
            BirthDate = n.BirthDate
        };

    private static List<string> ParseDelimited(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? []
            : [.. value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)];

    private static List<T> ParseEnumList<T>(string? value) where T : struct, Enum =>
        string.IsNullOrWhiteSpace(value)
            ? []
            : [.. value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                       .Select(s => Enum.TryParse<T>(s, out var v) ? (T?)v : null)
                       .OfType<T>()];
}
