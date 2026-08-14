(function () {
    const root = document.getElementById('chatWidget');
    if (!root || typeof signalR === 'undefined') return;

    const myId = parseInt(root.dataset.userId, 10);
    const csrf = root.dataset.csrf;

    const launcher = document.getElementById('chatwLauncher');
    const badge = document.getElementById('chatwBadge');
    const panel = document.getElementById('chatwPanel');
    const backBtn = document.getElementById('chatwBack');
    const deleteBtn = document.getElementById('chatwDelete');
    const closeBtn = document.getElementById('chatwClose');
    const title = document.getElementById('chatwTitle');
    const headerAvatar = document.getElementById('chatwHeaderAvatar');
    const listEl = document.getElementById('chatwList');
    const threadEl = document.getElementById('chatwThread');
    const messagesEl = document.getElementById('chatwMessages');
    const composer = document.getElementById('chatwComposer');
    const input = document.getElementById('chatwInput');

    let currentConversationId = null;

    const connection = new signalR.HubConnectionBuilder()
        .withUrl('/hubs/chat')
        .withAutomaticReconnect()
        .build();

    function setBadge(count) {
        badge.hidden = count <= 0;
        badge.textContent = count > 99 ? '99+' : String(count);
    }

    async function refreshBadge() {
        try {
            const res = await fetch('/Chat/UnreadCount');
            if (res.ok) setBadge((await res.json()).count);
        } catch {  }
    }

    function fmtTime(iso) {
        return new Date(iso).toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' });
    }

    function fmtListTime(iso) {
        const d = new Date(iso);
        return d.toDateString() === new Date().toDateString()
            ? fmtTime(iso)
            : d.toLocaleDateString('pt-BR', { day: '2-digit', month: '2-digit' });
    }

    async function loadConversations() {
        const res = await fetch('/Chat/Conversations');
        if (!res.ok) return;
        const conversations = await res.json();

        listEl.replaceChildren();

        if (conversations.length === 0) {
            const empty = document.createElement('div');
            empty.className = 'chatw__empty';
            empty.textContent = 'Nenhuma conversa ainda. Encontre um nutricionista e envie uma mensagem pelo perfil dele.';
            listEl.appendChild(empty);
            return;
        }

        for (const c of conversations) {
            const item = document.createElement('div');
            item.className = 'chatw__item';
            item.setAttribute('role', 'button');
            item.tabIndex = 0;

            const avatar = document.createElement('span');
            avatar.className = 'nc-avatar nc-avatar--sm chatw__avatar';
            avatar.textContent = c.otherInitials;

            const info = document.createElement('span');
            info.className = 'chatw__iteminfo';

            const name = document.createElement('span');
            name.className = 'chatw__itemname';
            name.textContent = c.otherName;

            const preview = document.createElement('span');
            preview.className = 'chatw__itempreview';
            preview.textContent = c.lastMessagePreview ?? 'Conversa iniciada';

            info.append(name, preview);

            const meta = document.createElement('span');
            meta.className = 'chatw__itemmeta';

            const time = document.createElement('span');
            time.className = 'chatw__itemtime';
            time.textContent = fmtListTime(c.lastMessageAt);
            meta.appendChild(time);

            if (c.unreadCount > 0) {
                const unread = document.createElement('span');
                unread.className = 'chatw__itemunread';
                unread.textContent = c.unreadCount > 99 ? '99+' : String(c.unreadCount);
                meta.appendChild(unread);
            }

            const del = document.createElement('button');
            del.type = 'button';
            del.className = 'chatw__itemdel';
            del.setAttribute('aria-label', 'Excluir conversa com ' + c.otherName);
            del.innerHTML = '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="3 6 5 6 21 6" /><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" /></svg>';
            del.addEventListener('click', e => {
                e.stopPropagation();
                deleteConversation(c.conversationId);
            });

            item.append(avatar, info, meta, del);
            item.addEventListener('click', () => openThread(c.conversationId));
            item.addEventListener('keydown', e => {
                if (e.key === 'Enter' || e.key === ' ') {
                    e.preventDefault();
                    openThread(c.conversationId);
                }
            });
            listEl.appendChild(item);
        }
    }

    function appendMessage(msg) {
        const bubble = document.createElement('div');
        bubble.className = 'chatw__msg ' + (msg.senderId === myId ? 'chatw__msg--mine' : 'chatw__msg--theirs');

        const text = document.createElement('span');
        text.className = 'chatw__msgtext';
        text.textContent = msg.content;

        const time = document.createElement('span');
        time.className = 'chatw__msgtime';
        time.textContent = fmtTime(msg.sentAt);

        bubble.append(text, time);
        messagesEl.appendChild(bubble);
    }

    function scrollToBottom() {
        messagesEl.scrollTop = messagesEl.scrollHeight;
    }

    async function openThread(conversationId) {
        const res = await fetch('/Chat/Messages/' + conversationId);
        if (!res.ok) return;
        const thread = await res.json();

        currentConversationId = thread.conversationId;

        title.textContent = thread.otherName;
        headerAvatar.textContent = thread.otherInitials;
        headerAvatar.hidden = false;
        backBtn.hidden = false;
        deleteBtn.hidden = false;
        listEl.hidden = true;
        threadEl.hidden = false;

        messagesEl.replaceChildren();
        thread.messages.forEach(appendMessage);
        scrollToBottom();
        input.focus();

        try { await connection.invoke('JoinConversation', currentConversationId); } catch { }
        refreshBadge();
    }

    async function backToList() {
        if (currentConversationId !== null) {
            try { await connection.invoke('LeaveConversation', currentConversationId); } catch { }
            currentConversationId = null;
        }
        title.textContent = 'Mensagens';
        headerAvatar.hidden = true;
        backBtn.hidden = true;
        deleteBtn.hidden = true;
        threadEl.hidden = true;
        listEl.hidden = false;
        loadConversations();
    }

    function openPanel() {
        panel.hidden = false;
        launcher.setAttribute('aria-expanded', 'true');
        loadConversations();
    }

    function closePanel() {
        panel.hidden = true;
        launcher.setAttribute('aria-expanded', 'false');
        if (currentConversationId !== null) backToList();
    }

    async function deleteConversation(conversationId) {
        const ok = await window.ncConfirm({
            title: 'Excluir conversa',
            message: 'A conversa some da sua lista; o outro participante mantém o histórico. Se ele enviar uma mensagem nova, a conversa volta a aparecer para você.',
            okLabel: 'Excluir',
            danger: true
        });
        if (!ok) return;

        const res = await fetch('/Chat/Delete', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'RequestVerificationToken': csrf },
            body: 'conversationId=' + encodeURIComponent(conversationId)
        });
        if (!res.ok) return;

        if (conversationId === currentConversationId) {
            await backToList();
        } else {
            loadConversations();
        }
        refreshBadge();
    }

    launcher.addEventListener('click', () => (panel.hidden ? openPanel() : closePanel()));
    closeBtn.addEventListener('click', closePanel);
    backBtn.addEventListener('click', backToList);
    deleteBtn.addEventListener('click', () => {
        if (currentConversationId !== null) deleteConversation(currentConversationId);
    });

    composer.addEventListener('submit', async e => {
        e.preventDefault();
        const content = input.value.trim();
        if (!content || currentConversationId === null) return;
        input.value = '';
        try { await connection.invoke('SendMessage', currentConversationId, content); } catch { }
    });

    connection.on('ReceiveMessage', async msg => {
        if (!threadEl.hidden && msg.conversationId === currentConversationId) {
            appendMessage(msg);
            scrollToBottom();
            if (msg.senderId !== myId) {
                try { await fetch('/Chat/Messages/' + msg.conversationId); } catch { }
                refreshBadge();
            }
        } else if (!panel.hidden && threadEl.hidden) {
            loadConversations();
        }
    });

    connection.on('UnreadChanged', count => {
        setBadge(count);
        if (!panel.hidden && threadEl.hidden) loadConversations();
    });

    window.ncChat = {
        async startWith(nutritionistId, initialMessage) {
            const res = await fetch('/Chat/Start', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'RequestVerificationToken': csrf },
                body: 'nutritionistId=' + encodeURIComponent(nutritionistId)
            });
            if (!res.ok) return;
            const { conversationId } = await res.json();
            if (panel.hidden) openPanel();
            await openThread(conversationId);
            if (initialMessage) {
                try { await connection.invoke('SendMessage', conversationId, initialMessage); } catch { }
            }
        }
    };

    connection.start().then(refreshBadge).catch(() => { });
})();
