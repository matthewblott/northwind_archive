// replaced DOMContentLoaded with turbolinks:load
document.addEventListener('turbolinks:load', () => {

  document.querySelectorAll('input,textarea,select').forEach((el) =>
    el.addEventListener('invalid', (e) => e.preventDefault(), true));

  const selector1 = 'data-nw-compare';

  document.querySelectorAll(`input[${selector1}]`).forEach((c) => {
    const p = document.getElementById(c.attributes[selector1].value);
    const e = () => c.setCustomValidity(c.value === p.value ? '' : '.');

    p.addEventListener('change', e, true);
    c.addEventListener('keyup', e, true);

  });

  const selector2 = 'data-nw-action';

  document.querySelectorAll(`input[${selector2}]`).forEach((el) => {

    el.addEventListener('keyup', () => {

      const action = el.attributes[selector2].value;

      const additionalFields = el.getAttribute('data-nw-additional-fields');

      const frm = el.closest('form');

      let urlSuffix = '';

      if(additionalFields !== null) {

        const fields = additionalFields.split(',');

        fields.forEach((fieldName) => {

          const field = frm.querySelector(`#${fieldName}`);

          if(field !== null) {
            urlSuffix += `&${fieldName}=${field.value}`;
          }

        });

      }

      fetch(`${action}?${el.id}=${el.value}${urlSuffix}`).then((resp) => resp.json()).then((j) => {
        el.setCustomValidity(j ? '.' : '');
      });

    }, true);

  });


});