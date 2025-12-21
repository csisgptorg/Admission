document.addEventListener('DOMContentLoaded', function () {
    setTimeout(function () {
        const originalFetch = window.fetch;
        window.fetch = async function (input, init) {
            const response = await originalFetch(input, init);
            const responseClone = response.clone();
            if ((typeof input === 'string' && input.includes('/auth/login')) ||
                (typeof input === 'object' && input.url && input.url.includes('/auth/login'))) {
                if (response.headers.get('content-type').includes('application/json')) {
                    if (response.status === 200) {
                        const json = await responseClone.json();
                        const token = json?.data?.tokenInfo?.jwToken;
                        if (token) {
                            ui.preauthorizeApiKey('Bearer', token);
                            if (localStorage && ui.getConfigs().persistAuthorization) {
                                localStorage.setItem('authorized', JSON.stringify(ui.authSelectors.authorized().toJS()))
                            }

                            new Toast("توکن احراز هویت با موفقیت دریافت و تنظیم شد.", ToastType.Success, 5000);
                        }
                    } else {
                        new Toast("مشخصات کاربری وارد شده اشتباه است.", ToastType.Danger, 5000);
                    }
                }
            }

            return response;
        };
    }, 3000);
})