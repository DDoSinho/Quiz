$(document).ready(function () {
    $("#nextquestionbutton").on('click', function (e) {

        var countCheckBoxes = 0;

        $("input.checkbox").each(function (i) {
            if ($(this).is(":visible") && $(this).is(":checked")) {
                ++countCheckBoxes;
            }
        });

        if (countCheckBoxes > 0) {
            $(this).submit(); //if the submit button can not submit because of preventDefault
        }
        else {
            e.preventDefault(); //if the user does not check any checkbox, we dont have to submit
            var errorMessage = $("<div>You have to give at least one answer!</div>")
            $("#nextquestiondiv").append(errorMessage);
        }

    });
});
