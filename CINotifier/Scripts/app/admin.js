$(function () {
    var viewModel = new adminViewModel();
    ko.applyBindings(viewModel);

    $.getJSON('/api/builds', function (data) {
        viewModel.projects(data);
    });
});