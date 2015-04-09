function notifierViewModel() {
    var self = this;

    function getProject(name) {
        var projects = self.projects().filter(function(project) {
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

    self.isLastSuccess = function(name) {
        var project = getProject(name);
        if (project) {
            return project.IsLastSuccess;
        }

        return true;
    };

    self.lastFailedDeveloper = function(name) {
        if (!self.isLastSuccess(name)) {
            var failedBuild = getLastFailedFromProject(name);
            if (failedBuild) {
                return failedBuild.Developer.Name;
            }
        }

        return null;
    };

    self.lastFailedVersion = function(name) {
        if (!self.isLastSuccess(name)) {
            var failedBuild = getLastFailedFromProject(name);
            if (failedBuild) {
                return 'Build: ' + failedBuild.Number;
            }
        };

        return null;
    }
}