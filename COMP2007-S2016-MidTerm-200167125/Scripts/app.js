$(document).ready(function () {

    console.log("app started");

    $("delete").click(function () {
        return confirm("Are you sure you want to delete this record?");
    });
});