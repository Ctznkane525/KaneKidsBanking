// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function EditCurrentItem(btn) {
    row = $(btn).closest('tr');
    guid = row.find(".trans-item-guid").html();
    name = row.find(".trans-item-name").html();
    date = row.find(".trans-item-date").html();
    amt = row.find(".trans-item-amount").html();
    return EditItem(guid, name, date, amt);
}

function InsertItem() {
    return EditItem("", "Name of Transaction", moment(new Date()).format("MM/DD/YYYY"), "0.00");
}

function EditItem(guid, name, date, amount) {
    $('#edit-guid').val(guid);
    $('#edit-name').val(name);
    $('#edit-date').val(date);
    $('#edit-amount').val(amount);
    $('#edit-form').show();
    return false;
}