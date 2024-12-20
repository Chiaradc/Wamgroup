﻿
(function () {
   function init() {
      var captchaForms = [];
      var formContainers = document.querySelectorAll('[data-sf-role="form-container"]');

      formContainers.forEach(container => {
         var hasCaptcha = container.parentNode.querySelector('[data-sf-role="captcha"]');
         if (hasCaptcha) {
            captchaForms.push(container.parentNode);
         }
      });

      captchaForms.forEach((form, i) => {
         var submitButton = form.querySelector('[data-sf-role="submit-button-container"] button');
         if (submitButton) {
            var submitId = 'onCaptchaSubmit' + i;
            window[submitId] = function () {
               form.dispatchEvent(new Event('submit'));
            };
            submitButton.classList.add('g-recaptcha');
            submitButton.setAttribute('data-sitekey', '6LdzVvIpAAAAACKF5Gz0VB1YK-0_kk8t8B_FxTsO');
               submitButton.setAttribute('data-callback', submitId);
            }
        });
    }
               document.addEventListener('DOMContentLoaded', init);
})();