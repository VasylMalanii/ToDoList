// test data
// todo replace with API call. Try to return data in this format so we don't have to change anything
var categories = [];
var category1 = {};
category1.id = 1;
category1.name = "Category 1";
category1.description = "Very nice.";
category1.tasks = [];
var task1 = {};
task1.id = 1;
task1.name = "Extreme Programming - Lab 2";
task1.description = "Do nothing for 3 weeks, then do nothing some more";
var task2 = {};
task2.id = 2;
task2.name = "Code refactoring - Lab 3";
task2.description = "Do nothing for 2 weeks, then do nothing some more";
var task3 = {};
task3.id = 3;
task3.name = "Jira and Redmine - Lab 4";
task3.description = "Do nothing for 1 week...";
category1.tasks.push(task1);
category1.tasks.push(task2);
category1.tasks.push(task3);

var category2 = {};
category2.id = 2;
category2.name = "Category 2";
category2.description = "Worse.";
category2.tasks = [];
var task4 = {};
task4.id = 4;
task4.name = "Useless";
task4.description = "Why are we still here";
var task5 = {};
task5.id = 5;
task5.name = "Facts";
task5.description = "PES >>>> FIFA";
var task6 = {};
task6.id = 6;
task6.name = "STOP";
task6.description = "I'm waiting for the worms";
//category2.tasks.push(task4);
category2.tasks.push(task5);
category2.tasks.push(task6);

categories.push(category1);
categories.push(category2);

$(document).ready(function() {
    // todo get this from ajax call
    $(".category-container").html(getCategoriesHtml(categories));
});

function addTaskToCategory(categoryId) {
    var newTaskForm = $(".category[data-category-id='" + categoryId + "'] .new-task");

    var task = {};
    task.name = newTaskForm.find(".new-task-header input").val();
    task.description = newTaskForm.find(".new-task-body textarea").val();

    if (task.name === "" || task.description === "") {
        displayError("Name and Description fields are required");
        return;
    }

    //Make API call to save task here. Saved Task Id should be returned.
    //following code should be executed on request success. Error should be handled with displayMessage function
    newTaskForm.find(".new-task-header > input").val("");
    newTaskForm.find(".new-task-body > textarea").val("");
    var idFromRequest = 1232; // todo change this to real saved id from request
    task.id = idFromRequest;
    var newTaskElement = $(getTaskHtml(task)).hide();
    newTaskElement.insertBefore(newTaskForm).slideDown('fast');
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

    //Make API call to save task here.
    //following code should be executed on request success. Error should be handled with displayMessage function
    editElement.remove();
    taskElement.show();
    taskElement.find(".task-header").text(task.name);
    taskElement.find(".task-body").text(task.description);
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

    //Make API call to save category here. Saved Category Id should be returned.
    //following code should be executed on request success. Error should be handled with displayMessage function
    newCategoryForm.find(".new-category-header > input").val("");
    newCategoryForm.find(".new-category-body > textarea").val("");
    var idFromRequest = 233343; // todo change this to real saved id from request
    category.id = idFromRequest;
    var categoryElement = $(getCategoryHtml(category));
    categoryElement.insertBefore(newCategoryForm);
}

function deleteTask(taskId) {
    var taskElement = $(".task[data-task-id='" + taskId + "']");

    //Make API call to delete task here.
    //following code should be executed on request success. Error should be handled with displayMessage function
    taskElement.slideUp('fast', function() {
        $(this).remove();
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