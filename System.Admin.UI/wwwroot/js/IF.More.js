$(document).ready(function () {
    $(".content").hide();
    $(".show_hide").on("click", function (e) {

        e.preventDefault();
        var txt = $(".content").is(':visible') ? 'Read More' : 'Read Less';
        $(".show_hide").text(txt);
        $(this).next('.content').slideToggle(200);
    });
});