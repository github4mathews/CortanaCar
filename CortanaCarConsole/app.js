var CameraCarApp;
(function (CameraCarApp) {
    var wireUpMotorActon = function (selector, action) {
        $(selector).on('mousedown touchstart', function (e) {
            e.preventDefault();
            $.post('/Car/' + action).fail(function () { alert('Fail ' + action); });
        }).on('mouseup touchend', function (e) {
            e.preventDefault();
            $.post('/Car/StopAll').fail(function () { alert('Fail StopAll'); });
        }).on('click contextmenu', function (e) {
            e.preventDefault();
        });
    };
    wireUpMotorActon('#btn-move-forward', 'StartMoveForward');
    wireUpMotorActon('#btn-move-backward', 'StartMoveBackward');
    wireUpMotorActon('#btn-turn-left', 'StartTurnLeft');
    wireUpMotorActon('#btn-turn-right', 'StartTurnRight');
})(CameraCarApp || (CameraCarApp = {}));