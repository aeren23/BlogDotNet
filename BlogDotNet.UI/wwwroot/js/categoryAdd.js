$(document).ready(function () {

    $("#btnSave").click(function (event) {
        event.preventDefault();

        var addUrl = app.Urls.categoryAddUrl;
        var redirectUrl = app.Urls.articleAddUrl;

        console.log("Add URL:", addUrl);
        console.log("Redirect URL:", redirectUrl);

        var categoryAddDto = {
            Name: $("input[id=categoryName]").val(),
        }
        console.log(categoryAddDto);
        var jsonData = JSON.stringify(categoryAddDto);
        console.log(jsonData);

        $.ajax({
            url: addUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: {
                "Accept": "application/json"
            },
            data: jsonData,
            success: function (data) {
                setTimeout(function () {
                    window.location.href = redirectUrl;
                }, 1500);
            },
            error: function (xhr, status, error) {
                console.log("Hata Aldın Dostum");
                console.log("Status: " + status);
                console.log("Error: " + error);
                console.log("Response: " + xhr.responseText);

                toastr.error("Bir Hata Oluştu: " + xhr.responseText, "Hatalarrrr");
            }
        });
    });
});