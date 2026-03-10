// ============================================================
// EJDER BACKOFFICE FRONTEND CONTROLLER
// - Theme management
// - Sidebar behavior
// - Dashboard terminal effects
// - DataTables / SweetAlert2 / Toastr helpers
// ============================================================

(function () {
    // --------------------------------------------------------
    // 1. THEME MANAGEMENT
    // --------------------------------------------------------

    const THEME_KEY = "ejder-theme";
    const root = document.documentElement;

    function getSavedTheme() {
        const stored = window.localStorage.getItem(THEME_KEY);
        if (stored === "dark" || stored === "light") {
            return stored;
        }
        return "light";
    }

    function applyTheme(theme) {
        root.setAttribute("data-theme", theme);
        window.localStorage.setItem(THEME_KEY, theme);
        updateThemeButton(theme);
        updateThirdPartyTheme(theme);
    }

    function updateThemeButton(theme) {
        const btn = document.getElementById("ej-theme-toggle");
        if (!btn) return;

        if (theme === "dark") {
            btn.innerHTML = '<span>☀️</span><span>Light Mod</span>';
        } else {
            btn.innerHTML = '<span>🌙</span><span>Dark Mod</span>';
        }
    }

    function updateThirdPartyTheme(theme) {
        // SweetAlert2 basic theme sync
        if (window.Swal && window.Swal.mixin) {
            const isDark = theme === "dark";
            window.EjderSwal = window.Swal.mixin({
                background: isDark ? "#111111" : "#ffffff",
                color: isDark ? "#ffffff" : "#333333",
                confirmButtonColor: isDark ? "#00FF88" : "#2E86C1",
                cancelButtonColor: isDark ? "#FF4444" : "#E74C3C"
            });
        }

        // Toastr
        if (window.toastr) {
            const isDark = theme === "dark";
            window.toastr.options = {
                positionClass: "toast-top-right",
                timeOut: 3000,
                closeButton: true,
                progressBar: true,
                newestOnTop: true,
                preventDuplicates: true,
                backgroundColor: isDark ? "#111111" : "#ffffff"
            };
        }
    }

    function initTheme() {
        const saved = getSavedTheme();
        applyTheme(saved);

        const btn = document.getElementById("ej-theme-toggle");
        if (btn) {
            btn.addEventListener("click", function () {
                const current = root.getAttribute("data-theme") || "light";
                const next = current === "light" ? "dark" : "light";
                applyTheme(next);
            });
        }
    }

    // --------------------------------------------------------
    // 2. SIDEBAR MANAGEMENT
    // --------------------------------------------------------

    function initSidebar() {
        const currentPath = window.location.pathname.toLowerCase();
        const items = document.querySelectorAll("[data-ej-nav]");

        items.forEach(function (item) {
            const href = (item.getAttribute("href") || "").toLowerCase();
            if (!href || href === "#") return;
            if (currentPath.startsWith(href)) {
                item.classList.add("ej-active");
            }
        });
    }

    // --------------------------------------------------------
    // 3. DASHBOARD TERMINAL EFFECTS
    // --------------------------------------------------------

    function initDashboardEffects() {
        const stream = document.getElementById("ej-inbound-stream");
        const statusLatency = document.getElementById("ej-status-latency");

        if (!stream && !statusLatency) {
            return;
        }

        const lines = [
            "[SYSTEM_NOTIFICATION] Inbound reservation feed attached.",
            "[SYSTEM_NOTIFICATION] Queue worker heartbeat OK.",
            "[SYSTEM_NOTIFICATION] Payment gateway latency within threshold.",
            "[SYSTEM_NOTIFICATION] Cache node resynced.",
            "[SYSTEM_NOTIFICATION] SMTP worker idle.",
            "[SYSTEM_NOTIFICATION] Core services nominal."
        ];

        function appendLine() {
            if (!stream) return;
            const index = Math.floor(Math.random() * lines.length);
            const line = document.createElement("div");
            line.textContent = lines[index];
            stream.appendChild(line);

            while (stream.children.length > 8) {
                stream.removeChild(stream.firstChild);
            }
        }

        appendLine();
        setInterval(appendLine, 3000);

        if (statusLatency) {
            setInterval(function () {
                var value = 10 + Math.floor(Math.random() * 10);
                statusLatency.textContent = value + "ms";
            }, 4000);
        }
    }

    // --------------------------------------------------------
    // 4. DATATABLES
    // --------------------------------------------------------

    function initDataTables() {
        if (!window.jQuery || !window.jQuery.fn || !window.jQuery.fn.DataTable) {
            return;
        }

        window.jQuery(".js-ej-datatable").each(function () {
            const $table = window.jQuery(this);
            if ($table.hasClass("dataTable-initialized")) return;
            $table.addClass("dataTable-initialized");

            const defaultOptions = {
                language: {
                    url: "//cdn.datatables.net/plug-ins/1.13.4/i18n/tr.json"
                },
                pageLength: 10,
                lengthMenu: [10, 25, 50, 100]
            };

            const orderCol = $table.data("order-col");
            const orderDir = $table.data("order-dir");
            if (orderCol !== undefined && orderDir) {
                defaultOptions.order = [[parseInt(orderCol, 10), orderDir]];
            }

            $table.DataTable(defaultOptions);
        });
    }

    // --------------------------------------------------------
    // 5. SWEETALERT DELETE HELPERS
    // --------------------------------------------------------

    function initDeleteButtons() {
        const handler = function (e) {
            e.preventDefault();
            const url = this.getAttribute("data-delete-url");
            const rowId = this.getAttribute("data-row-id");
            if (!url) return;

            const swal = window.EjderSwal || window.Swal;
            if (!swal) {
                window.location.href = url;
                return;
            }

            swal.fire({
                title: "Emin misiniz?",
                text: "Bu kayıt kalıcı olarak silinecektir!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Evet, sil!",
                cancelButtonText: "İptal"
            }).then(function (result) {
                if (!result.isConfirmed) return;

                window.jQuery.post(url, function (res) {
                    if (res && res.success) {
                        if (rowId) {
                            const row = document.getElementById(rowId);
                            if (row) {
                                row.classList.add("ej-fade-in");
                                row.style.opacity = "0";
                                setTimeout(function () {
                                    row.remove();
                                }, 250);
                            }
                        }

                        if (window.toastr) {
                            window.toastr.success(res.message || "Kayıt başarıyla silindi.");
                        }
                    } else {
                        const msg = (res && res.message) || "Silme işlemi başarısız.";
                        if (window.toastr) {
                            window.toastr.error(msg);
                        } else if (swal) {
                            swal.fire("Hata", msg, "error");
                        }
                    }
                });
            });
        };

        document.querySelectorAll(".js-ej-delete").forEach(function (btn) {
            btn.removeEventListener("click", handler);
            btn.addEventListener("click", handler);
        });
    }

    // --------------------------------------------------------
    // 6. TOAST FROM TEMPDATA
    // --------------------------------------------------------

    function showTempDataToasts() {
        if (!window.toastr) return;

        const success = document.body.getAttribute("data-temp-success");
        const error = document.body.getAttribute("data-temp-error");
        const warning = document.body.getAttribute("data-temp-warning");

        if (success) window.toastr.success(success);
        if (error) window.toastr.error(error);
        if (warning) window.toastr.warning(warning);
    }

    // --------------------------------------------------------
    // 7. BOOTSTRAP
    // --------------------------------------------------------

    document.addEventListener("DOMContentLoaded", function () {
        initTheme();
        initSidebar();
        initDashboardEffects();
        initDataTables();
        initDeleteButtons();
        showTempDataToasts();
    });

})();

