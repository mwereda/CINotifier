function notifierViewModel() {
    var self = this;

    function getProject(name) {
        var projects = self.projects().filter(function (project) {
            return project.Project.Name === name;
        });

        return projects[0];
    }

    function getLastFailedFromProject(name) {
        var project = getProject(name);
        if (project) {
            return project.Builds[0];
        }

        return null;
    }

    self.projects = ko.observableArray([]);

    self.isLastSuccess = ko.computed({
        read: function () {
            return true;
        },
        write: function (name) {
            var project = getProject(name);
            if (project) {
                return project.IsLastSuccess;
            }

            return true;
        },
        owner: this
    });

    self.lastFailedDeveloper = ko.computed(
    {
        read: function () {
            return '';
        },
        write: function (name) {
            if (!self.isLastSuccess()) {
                var failedBuild = getLastFailedFromProject(name);
                if (failedBuild) {
                    return failedBuild.Developer.Name;
                }
            }

            return null;
        },
        owner: this
    });

    self.lastFailedVersion = ko.computed({
        read: function () {
            return '';
        },
        write: function (name) {
            if (!self.isLastSuccess()) {
                var failedBuild = getLastFailedFromProject(name);
                if (failedBuild) {
                    return failedBuild.Number;
                }
            }

            return null;
        },
        owner: this
    });
}