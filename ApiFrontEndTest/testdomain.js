

$(document).ready(function () {
    $("button").click(function () {
        $.post('http://localhost:82/Catalogs/GetSubDomain',
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