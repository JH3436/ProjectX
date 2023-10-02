document.addEventListener('DOMContentLoaded', function () {
    const startDate = document.getElementById("StartDate");
    const endDate = document.getElementById("EndDate");

    let today = new Date();
    let nextMonth = new Date(today);
    nextMonth.setMonth(today.getMonth() + 1);

    startDate.min = formatDate(nextMonth);

    startDate.addEventListener("change", function () {
        let selectedDate = new Date(this.value);
        selectedDate.setDate(selectedDate.getDate() + 1);
        endDate.min = formatDate(selectedDate);
        endDate.disabled = false;
    });
});

function formatDate(date) {
    let d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}