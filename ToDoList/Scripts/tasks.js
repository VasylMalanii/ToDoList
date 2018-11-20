var accountId = $("#hiddenAccountId").val();

if (accountId === "" || accountId === null || accountId === undefined) {
    $(".authorized").hide();
    window.location = "/home";
} else {
    $(".authorized").show();
}

$(document).ready(function () {  
    $.ajax({
        type: 'GET',
        url: '/get-account-categories',
        success: function (categories) {
            $(".category-container").html(getCategoriesHtml(categories));
        },
        error: function(error) {
            displayError("Unable to load data. Please try to refresh this page or relogin");
        }
    });
});

function addTaskToCategory(categoryId) {
    var newTaskForm = $(".category[data-category-id='" + categoryId + "'] .new-task");

    var task = {};
    task.categoryId = categoryId;
    task.name = newTaskForm.find(".new-task-header input").val();
    task.description = newTaskForm.find(".new-task-body textarea").val();

    if (task.name === "" || task.description === "") {
        displayError("Name and Description fields are required");
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/post-task',
        data: task,
        success: function (data) {
            newTaskForm.find(".new-task-header > input").val("");
            newTaskForm.find(".new-task-body > textarea").val("");
            task.id = data.id;
            var newTaskElement = $(getTaskHtml(task)).hide();
            newTaskElement.insertBefore(newTaskForm).slideDown('fast');
        },
        error: function (error) {
            displayError("Unexpected error while trying to create a new task.");
        }
    });
    
}

function enterEditMode(taskId) {
    var taskElement = $(".task[data-task-id='" + taskId + "']");

    var task = {};
    task.id = taskId;
    task.name = taskElement.find(".task-header").text();
    task.description = taskElement.find(".task-body").text();

    var editElement = $(getEditTaskHtml(task));
    editElement.insertAfter(taskElement);
    taskElement.hide();

}

function cancelEditMode(taskId) {
    var taskElement = $(".task[data-task-id='" + taskId + "']");
    var editElement = $(".edit-task[data-task-id='" + taskId + "']");

    editElement.remove();
    taskElement.show();
}

function saveEdited(taskId) {
    var taskElement = $(".task[data-task-id='" + taskId + "']");
    var editElement = $(".edit-task[data-task-id='" + taskId + "']");

    var task = {};
    task.id = taskId;
    task.name = editElement.find(".edit-task-header input").val();
    task.description = editElement.find(".edit-task-body textarea").val();
    if (task.name === "" || task.description === "") {
        displayError("Name and Description fields are required");
        return;
    }

    $.ajax({
        type: 'PUT',
        url: '/update-task',
        data: task,
        success: function(data) {
            editElement.remove();
            taskElement.show();
            taskElement.find(".task-header").text(task.name);
            taskElement.find(".task-body").text(task.description);
        },
        error: function (error) {
            displayError("Unexpected error while trying to update a task.");
        }
    });
}

function addNewCategory() {
    var newCategoryForm = $(".new-category");

    var category = {};
    category.name = $(".new-category").find(".new-category-header input").val();
    category.description = $(".new-category").find(".new-category-body textarea").val();
    category.tasks = [];
    if (category.name === "" || category.description === "") {
        displayError("Name and Description fields are required");
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/post-category',
        data: category,
        success: function (data) {
            newCategoryForm.find(".new-category-header > input").val("");
            newCategoryForm.find(".new-category-body > textarea").val("");
            category.id = data.id;
            var categoryElement = $(getCategoryHtml(category));
            categoryElement.insertBefore(newCategoryForm);
        },
        error: function (error) {
            displayError("Unexpected error while trying to create a new category.");
        }
    });
}

function deleteTask(taskId) {
    var taskElement = $(".task[data-task-id='" + taskId + "']");

    var data = "taskId=" + taskId;
    $.ajax({
        type: 'DELETE',
        url: '/delete-task',
        data: data,
        success: function(data) {
            taskElement.slideUp('fast',
                function() {
                    $(this).remove();
                });
        },
        error: function(error) {
            displayError("Unexpected error while trying to delete a task.");
        }
    });

}

function getTaskHtml(task) {
    var html =
        '<div class="task" data-task-id="' + task.id + '">' +
            '<div class="task-header">' +
            task.name +
            '</div>' +
            '<div class="task-body">' +
            task.description +
            '</div>' +
            '<div class="task-footer">' +
            '    <a role="button" class="label label-info" data-task-id="' + task.id + '" onclick="enterEditMode($(this).data(\'task-id\'))">Edit</a>' +
            '    <a role="button" class="label label-danger" data-task-id="' + task.id + '" onclick="deleteTask($(this).data(\'task-id\'))">Delete</a>' +
            '</div>' +
        '</div>';
    return html;
}

function getNewTaskHtml(categoryId) {
    var html =
        '<div class="new-task">' +
            '<div class= "new-task-header">' +
            '    <input type="text" class="form-control" placeholder="New Task Name" data-category-id="' + categoryId + '" />' +
            '</div>' +
            '<div class="new-task-body">' +
            '    <textarea class="form-control" data-category-id="' + categoryId + '" placeholder="Description"></textarea>' +
            '</div>' +
            '<div class="new-task-footer">' +
            '    <button class="btn btn-success btn-sm" data-category-id="' + categoryId + '" onclick="addTaskToCategory($(this).data(\'category-id\'))">Add Task</button>' +
            '</div>' +
            '</div >';
    return html;
}

function getEditTaskHtml(task) {
    var html =
        '<div class="edit-task" data-task-id="' + task.id + '">' +
            '<div class= "edit-task-header">' +
            '    <input type="text" class="form-control" data-task-id="' + task.id + '" value="' + task.name + '"/>' +
            '</div>' +
            '<div class="edit-task-body">' +
            '    <textarea class="form-control" data-task-id="' + task.id + '">' + task.description + '</textarea>' +
            '</div>' +
            '<div class="new-task-footer">' +
            '    <a role="button" class="label label-warning" data-task-id="' + task.id + '" onclick="cancelEditMode($(this).data(\'task-id\'))">Cancel</a>' +
            '    <a role="button" class="label label-success" data-task-id="' + task.id + '" onclick="saveEdited($(this).data(\'task-id\'))">Edit Task</a>' +
            '</div>' +
            '</div >';
    return html;
}

function getCategoryHtml(category) {
    var tasksHtml = "";
    category.tasks.forEach(function(task) {
        tasksHtml += getTaskHtml(task);
    });
    var newTaskHtml = getNewTaskHtml(category.id);
    var html =
        '<div class="category" data-category-id="' + category.id + '">' +
            '   <h4>' + category.name + '</h4>' +
            '   <p>' + category.description + '</p>' +
            tasksHtml +
            newTaskHtml +
        '</div>';
    return html;
}

function getNewCategoryHtml() {
    var html = 
        '<div class="new-category">' +
        '<div class= "new-category-header">' +
        '    <input type="text" class="form-control" placeholder="New Category Name"/>' +
        '</div>' +
        '<div class="new-category-body">' +
        '    <textarea class="form-control" placeholder="Description"></textarea>' +
        '</div>' +
        '<div class="new-category-footer">' +
        '    <button class="btn btn-success btn-sm" onclick="addNewCategory()">Add Category</button>' +
        '</div>' +
        '</div >';
    return html;
}

function getCategoriesHtml(categories) {
    var html = "";
    categories.forEach(function(category) {
        html += getCategoryHtml(category);
    });
    html += getNewCategoryHtml();
    return html;
}

function displayError(message) {
    var html =
        '<div class="alert alert-danger alert-dismissible">' +
            '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>' +
            message +
        '</div>';
    $("#message-container").html(html);
}