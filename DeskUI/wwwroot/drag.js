window.trackPointerMove = function (dotnetRef) {
    window.addEventListener('pointermove', e => {
        dotnetRef.invokeMethodAsync('HandleMouseMoveAsync', e.clientX, e.clientY);
    });
};
