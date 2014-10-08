$(document).ready(function()
{
    //Increase likes by one on click
    $('div').on('click', 'span.like', function () {
        var postID = $(this).data('postId');
        $.get(postID, function (data) {
            alert('good job');
        });
    });

    //Show & hide comments
    $('.showComments').on('click', function () {
        var parent = $(this).parent();
        var commentsToDisplay = parent.find('.commentsDiv');
        commentsToDisplay.slideToggle();
    });

    $('body').on('click', '.ajax-get', function () {
        var urlRequest = $(this).data('url');
        $.get(urlRequest, function (data) {
            $('#content').html(data);
        });
    });
});