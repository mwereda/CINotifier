$(function () {
    var viewModel = new notifierViewModel();
    ko.applyBindings(viewModel);

    function getProjects() {
        $.getJSON('/api/builds', function(data) {
            viewModel.projects(data);
        });
    }

    var hub = $.connection.builds;
    hub.client.addBuild = function() {
        getProjects();
    };

    $.connection.hub.start().done(getProjects());
});