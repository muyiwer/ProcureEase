

$(document).ready(function () {
    $("button").click(function () {
        $.post('http://localhost:82/Catalogs/Add',
            {
                headers: {
                    "contentType": "application/json"
                },
                dataType: 'jsonp'
            },
            function (data, status) {
                alert("SubDomain Name " + data + "\nStatus: " + status);
            }).fail(function(){
                alert("error")
            });
    });
});