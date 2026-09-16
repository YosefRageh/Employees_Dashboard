"use strict";

/* =========================================================
   APPLICATION UI
   ========================================================= */

document.addEventListener("DOMContentLoaded", () => {

    const UI = {

        sidebar: document.getElementById("appSidebar"),
        sidebarOverlay: document.getElementById("sidebarOverlay"),
        sidebarClose: document.getElementById("sidebarClose"),
        mobileMenuButton: document.getElementById("mobileMenuButton"),

        searchTrigger: document.getElementById("searchTrigger"),
        searchOverlay: document.getElementById("searchOverlay"),
        searchBackdrop: document.getElementById("searchBackdrop"),
        searchClose: document.getElementById("searchClose"),
        searchInput: document.getElementById("globalSearchInput"),

        notificationTrigger:
            document.getElementById("notificationTrigger"),

        notificationPanel:
            document.getElementById("notificationPanel"),

        notificationBadge:
            document.getElementById("notificationBadge"),

        markAllRead:
            document.getElementById("markAllRead"),

        notificationList:
            document.getElementById("notificationList"),

        accountTrigger:
            document.getElementById("accountTrigger"),

        accountPanel:
            document.getElementById("accountPanel"),

        themeToggle:
            document.getElementById("themeToggle"),

        themeIcon:
            document.getElementById("themeIcon"),

        robotAssistant:
            document.getElementById("robotAssistant"),

        robotButton:
            document.getElementById("robotButton"),

        robotSpeech:
            document.getElementById("robotSpeech")
    };


    /* =========================================================
       SIDEBAR
       ========================================================= */

    function openSidebar() {

        if (!UI.sidebar) {
            return;
        }

        UI.sidebar.classList.add("open");
        UI.sidebarOverlay?.classList.add("open");

        UI.mobileMenuButton?.setAttribute(
            "aria-expanded",
            "true"
        );

        document.body.style.overflow = "hidden";
    }


    function closeSidebar() {

        if (!UI.sidebar) {
            return;
        }

        UI.sidebar.classList.remove("open");
        UI.sidebarOverlay?.classList.remove("open");

        UI.mobileMenuButton?.setAttribute(
            "aria-expanded",
            "false"
        );

        document.body.style.overflow = "";
    }


    UI.mobileMenuButton?.addEventListener(
        "click",
        openSidebar
    );

    UI.sidebarClose?.addEventListener(
        "click",
        closeSidebar
    );

    UI.sidebarOverlay?.addEventListener(
        "click",
        closeSidebar
    );


    /* =========================================================
       SEARCH
       ========================================================= */

    function openSearch() {

        if (!UI.searchOverlay) {
            return;
        }

        closeAllDropdowns();

        UI.searchOverlay.classList.add("open");
        UI.searchOverlay.setAttribute(
            "aria-hidden",
            "false"
        );

        document.body.style.overflow = "hidden";

        window.setTimeout(() => {

            UI.searchInput?.focus();

        }, 120);
    }


    function closeSearch() {

        if (!UI.searchOverlay) {
            return;
        }

        UI.searchOverlay.classList.remove("open");
        UI.searchOverlay.setAttribute(
            "aria-hidden",
            "true"
        );

        document.body.style.overflow = "";

        UI.searchInput?.blur();
    }


    UI.searchTrigger?.addEventListener(
        "click",
        openSearch
    );

    UI.searchClose?.addEventListener(
        "click",
        closeSearch
    );

    UI.searchBackdrop?.addEventListener(
        "click",
        closeSearch
    );


    /* =========================================================
       NOTIFICATIONS
       ========================================================= */

    function openNotifications() {

        closeAccount();

        UI.notificationPanel?.classList.add("open");

        UI.notificationTrigger?.setAttribute(
            "aria-expanded",
            "true"
        );
    }


    function closeNotifications() {

        UI.notificationPanel?.classList.remove("open");

        UI.notificationTrigger?.setAttribute(
            "aria-expanded",
            "false"
        );
    }


    UI.notificationTrigger?.addEventListener(
        "click",
        event => {

            event.stopPropagation();

            const isOpen =
                UI.notificationPanel?.classList.contains("open");

            if (isOpen) {
                closeNotifications();
            } else {
                openNotifications();
            }
        }
    );


    /* =========================================================
       MARK NOTIFICATIONS AS READ
       ========================================================= */

    UI.markAllRead?.addEventListener(
        "click",
        event => {

            event.stopPropagation();

            const unreadItems =
                UI.notificationList?.querySelectorAll(
                    ".notification-item.unread"
                );

            unreadItems?.forEach(item => {
                item.classList.remove("unread");
            });

            if (UI.notificationBadge) {

                UI.notificationBadge.classList.add(
                    "hidden"
                );

                UI.notificationBadge.textContent = "0";
            }
        }
    );


    /* =========================================================
       ACCOUNT
       ========================================================= */

    function openAccount() {

        closeNotifications();

        UI.accountPanel?.classList.add("open");

        UI.accountTrigger?.setAttribute(
            "aria-expanded",
            "true"
        );
    }


    function closeAccount() {

        UI.accountPanel?.classList.remove("open");

        UI.accountTrigger?.setAttribute(
            "aria-expanded",
            "false"
        );
    }


    UI.accountTrigger?.addEventListener(
        "click",
        event => {

            event.stopPropagation();

            const isOpen =
                UI.accountPanel?.classList.contains("open");

            if (isOpen) {
                closeAccount();
            } else {
                openAccount();
            }
        }
    );


    /* =========================================================
       DROPDOWN HELPERS
       ========================================================= */

    function closeAllDropdowns() {

        closeNotifications();
        closeAccount();
    }


    document.addEventListener(
        "click",
        event => {

            if (
                !event.target.closest(".notification-trigger") &&
                !event.target.closest(".notification-panel")
            ) {
                closeNotifications();
            }

            if (
                !event.target.closest(".account-trigger") &&
                !event.target.closest(".account-panel")
            ) {
                closeAccount();
            }
        }
    );


    /* =========================================================
       THEME
       ========================================================= */

    const THEME_KEY = "employees-theme";


    function applyTheme(theme) {

        const validTheme =
            theme === "light"
                ? "light"
                : "dark";

        document.documentElement.dataset.theme =
            validTheme;

        updateThemeIcon(validTheme);

        localStorage.setItem(
            THEME_KEY,
            validTheme
        );
    }


    function updateThemeIcon(theme) {

        if (!UI.themeIcon) {
            return;
        }

        UI.themeIcon.className =
            theme === "light"
                ? "bi bi-sun-fill"
                : "bi bi-moon-stars-fill";
    }


    function initializeTheme() {

        const serverTheme =
            document.documentElement.dataset.theme;

        const storedTheme =
            localStorage.getItem(THEME_KEY);

        if (storedTheme) {

            applyTheme(storedTheme);

            return;
        }

        applyTheme(
            serverTheme === "light"
                ? "light"
                : "dark"
        );
    }


    UI.themeToggle?.addEventListener(
        "click",
        () => {

            const currentTheme =
                document.documentElement.dataset.theme;

            applyTheme(
                currentTheme === "dark"
                    ? "light"
                    : "dark"
            );
        }
    );


    initializeTheme();


    /* =========================================================
       KEYBOARD SHORTCUTS
       ========================================================= */

    document.addEventListener(
        "keydown",
        event => {

            if (
                (event.ctrlKey || event.metaKey) &&
                event.key.toLowerCase() === "k"
            ) {

                event.preventDefault();

                openSearch();

                return;
            }


            if (event.key === "Escape") {

                closeSearch();
                closeAllDropdowns();
                closeSidebar();
            }
        }
    );


    /* =========================================================
       ROBOT ASSISTANT
       ========================================================= */

    const robotMessages = [
        "Hi! Welcome back 👋",
        "Need to manage employees? 🤖",
        "Everything looks good! ✨",
        "Have a productive day 🚀"
    ];

    let robotMessageIndex = 0;


    function changeRobotMessage() {

        if (!UI.robotSpeech) {
            return;
        }

        robotMessageIndex =
            (robotMessageIndex + 1) %
            robotMessages.length;

        UI.robotSpeech.textContent =
            robotMessages[robotMessageIndex];

        UI.robotAssistant?.classList.add(
            "speaking"
        );

        window.setTimeout(() => {

            UI.robotAssistant?.classList.remove(
                "speaking"
            );

        }, 2600);
    }


    UI.robotButton?.addEventListener(
        "click",
        () => {

            if (!UI.robotButton) {
                return;
            }

            UI.robotButton.classList.remove(
                "wave"
            );

            void UI.robotButton.offsetWidth;

            UI.robotButton.classList.add(
                "wave"
            );

            changeRobotMessage();

            window.setTimeout(() => {

                UI.robotButton?.classList.remove(
                    "wave"
                );

            }, 3000);
        }
    );


    /* =========================================================
       ROBOT MESSAGE ROTATION
       ========================================================= */

    window.setInterval(() => {

        if (
            !UI.robotAssistant ||
            UI.robotAssistant.matches(":hover")
        ) {
            return;
        }

        changeRobotMessage();

    }, 9000);


    /* =========================================================
       RIPPLE INTERACTION
       ========================================================= */

    document.addEventListener(
        "click",
        event => {

            const target =
                event.target.closest(
                    ".btn, button[type='submit']"
                );

            if (!target) {
                return;
            }

            const ripple =
                document.createElement("span");

            ripple.className =
                "ui-ripple";

            const rect =
                target.getBoundingClientRect();

            const size =
                Math.max(
                    rect.width,
                    rect.height
                );

            ripple.style.width =
                `${size}px`;

            ripple.style.height =
                `${size}px`;

            ripple.style.left =
                `${event.clientX - rect.left - size / 2}px`;

            ripple.style.top =
                `${event.clientY - rect.top - size / 2}px`;

            target.style.position =
                "relative";

            target.style.overflow =
                "hidden";

            target.appendChild(ripple);

            window.setTimeout(() => {
                ripple.remove();
            }, 550);
        }
    );


    /* =========================================================
       MOBILE RESIZE
       ========================================================= */

    window.addEventListener(
        "resize",
        () => {

            if (
                window.innerWidth > 850
            ) {
                closeSidebar();
            }
        }
    );

});
/* =========================================================
   PREMIUM MICRO-INTERACTIONS
   ========================================================= */

document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll("button, a").forEach(function (element) {

        element.addEventListener("pointerdown", function () {

            element.classList.add("is-pressed");

        });

        element.addEventListener("pointerup", function () {

            element.classList.remove("is-pressed");

        });

        element.addEventListener("pointerleave", function () {

            element.classList.remove("is-pressed");

        });

    });

});
/* =========================================================
   GLOBAL PREMIUM INTERACTIONS
   ========================================================= */

document.addEventListener("DOMContentLoaded", function () {

    /* -----------------------------------------------------
       Auto-close temporary alerts
       ----------------------------------------------------- */

    document.querySelectorAll(
        ".auth-alert, .create-alert, .alert"
    ).forEach(function (alert) {

        setTimeout(function () {

            if (!alert) {
                return;
            }

            alert.style.transition =
                "opacity .3s ease, transform .3s ease";

            alert.style.opacity = "0";
            alert.style.transform = "translateY(-5px)";

            setTimeout(function () {

                if (alert.parentNode) {
                    alert.parentNode.removeChild(alert);
                }

            }, 320);

        }, 5000);

    });


    /* -----------------------------------------------------
       Add ripple feedback to buttons
       ----------------------------------------------------- */

    document.querySelectorAll(
        ".btn, button, .primary-button, .secondary-button"
    ).forEach(function (button) {

        button.addEventListener(
            "click",
            function (event) {

                if (
                    button.disabled ||
                    button.classList.contains("no-ripple")
                ) {
                    return;
                }

                const rect =
                    button.getBoundingClientRect();

                const ripple =
                    document.createElement("span");

                const size =
                    Math.max(
                        rect.width,
                        rect.height
                    );

                ripple.style.width = size + "px";
                ripple.style.height = size + "px";

                ripple.style.position = "absolute";
                ripple.style.left =
                    (event.clientX - rect.left - size / 2) + "px";

                ripple.style.top =
                    (event.clientY - rect.top - size / 2) + "px";

                ripple.style.borderRadius = "50%";

                ripple.style.background =
                    "rgba(255,255,255,.14)";

                ripple.style.pointerEvents = "none";

                ripple.style.transform = "scale(0)";
                ripple.style.opacity = "1";

                ripple.style.transition =
                    "transform .55s ease, opacity .55s ease";

                if (
                    getComputedStyle(button).position ===
                    "static"
                ) {
                    button.style.position = "relative";
                }

                button.style.overflow = "hidden";

                button.appendChild(ripple);

                requestAnimationFrame(function () {

                    ripple.style.transform = "scale(1)";
                    ripple.style.opacity = "0";

                });

                setTimeout(function () {

                    if (ripple.parentNode) {
                        ripple.parentNode.removeChild(ripple);
                    }

                }, 600);

            }
        );

    });


    /* -----------------------------------------------------
       Add loaded state
       ----------------------------------------------------- */

    document.documentElement.classList.add(
        "app-loaded"
    );


    /* -----------------------------------------------------
       Keyboard escape for open panels
       ----------------------------------------------------- */

    document.addEventListener(
        "keydown",
        function (event) {

            if (event.key !== "Escape") {
                return;
            }

            document
                .querySelectorAll(
                    ".dropdown-panel.open"
                )
                .forEach(function (panel) {

                    panel.classList.remove("open");

                });

        }
    );


    /* -----------------------------------------------------
       Smooth anchor scrolling
       ----------------------------------------------------- */

    document.querySelectorAll(
        'a[href^="#"]'
    ).forEach(function (link) {

        link.addEventListener(
            "click",
            function (event) {

                const targetId =
                    link.getAttribute("href");

                if (
                    !targetId ||
                    targetId === "#"
                ) {
                    return;
                }

                const target =
                    document.querySelector(targetId);

                if (!target) {
                    return;
                }

                event.preventDefault();

                target.scrollIntoView({
                    behavior: "smooth",
                    block: "start"
                });

            }
        );

    });

});
/* =========================================================
   LOGIN / REGISTER LOADING EFFECT
   ========================================================= */

document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll(
        'form[action*="Login"], form[action*="Register"]'
    ).forEach(function (form) {

        form.addEventListener("submit", function () {

            const button =
                form.querySelector(
                    'button[type="submit"]'
                );

            if (!button) {
                return;
            }

            button.classList.add("is-loading");

            const text =
                button.querySelector("span");

            if (text) {

                if (
                    form.action.toLowerCase().includes("login")
                ) {
                    text.textContent = "Signing in...";
                }
                else {
                    text.textContent = "Creating account...";
                }

            }

        });

    });

});
