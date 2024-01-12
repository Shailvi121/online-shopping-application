$(document).ready(function () {
    $("#deleteModal").click(function () {
       
        if (confirm("Are you sure you want to delete?")) {
          
            DeleteConfirmed();
        }
    });
    var BaseUrl = "https://localhost:44314/api/";

    function DeleteConfirmed(itemId) {
        debugger
        $.ajax({
            type: "DELETE",
            url: `${BaseUrl}Category/Deleted/${itemId}`,
            success: function () {
                alert("Success!");

            },
            error: function (data) {
                alert("Unable to delete data");
            }
        });
    }
});


