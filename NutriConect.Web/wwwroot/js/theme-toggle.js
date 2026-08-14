(function () {
  const storageKey = 'nc-theme';
  const root = document.documentElement;

  function getPreferredTheme() {
    if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
      return 'dark';
    }

    return 'light';
  }

  function resolveTheme() {
    const saved = localStorage.getItem(storageKey);
    if (saved === 'dark' || saved === 'light') {
      return saved;
    }

    return getPreferredTheme();
  }

  function applyTheme(theme) {
    root.setAttribute('data-theme', theme);
    root.setAttribute('data-bs-theme', theme);

    const toggle = document.getElementById('themeToggle');
    if (!toggle) {
      return;
    }

    const isDark = theme === 'dark';
    toggle.setAttribute('aria-pressed', String(isDark));
    toggle.setAttribute('aria-label', isDark ? 'Ativar tema claro' : 'Ativar tema escuro');
    toggle.title = isDark ? 'Ativar tema claro' : 'Ativar tema escuro';
  }

  function setTheme(theme) {
    localStorage.setItem(storageKey, theme);
    applyTheme(theme);
  }

  applyTheme(resolveTheme());

  const toggle = document.getElementById('themeToggle');
  if (toggle) {
    toggle.addEventListener('click', function () {
      const current = root.getAttribute('data-theme') === 'dark' ? 'dark' : 'light';
      const next = current === 'dark' ? 'light' : 'dark';
      setTheme(next);
    });
  }

  if (window.matchMedia) {
    const media = window.matchMedia('(prefers-color-scheme: dark)');

    const onChange = function (event) {
      if (localStorage.getItem(storageKey) === 'dark' || localStorage.getItem(storageKey) === 'light') {
        return;
      }

      applyTheme(event.matches ? 'dark' : 'light');
    };

    if (typeof media.addEventListener === 'function') {
      media.addEventListener('change', onChange);
    } else if (typeof media.addListener === 'function') {
      media.addListener(onChange);
    }
  }
})();
