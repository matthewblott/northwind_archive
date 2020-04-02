// replaced DOMContentLoaded with turbolinks:load
document.addEventListener('turbolinks:load', () => {
  if(typeof bulmaCalendar !== 'undefined') {
    bulmaCalendar.attach('[type="date"]');
  }
});
