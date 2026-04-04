window.trackPointerMove = (dotnet) => {
    let dragging = false;

    window.onpointerdown = e => {
        dragging = true;
    };

    window.onpointermove = e => {
        if (dragging)
            dotnet.invokeMethodAsync('HandleMouseMoveAsync', e.clientX, e.clientY);
    };

    window.onpointerup = e => {
        dragging = false;
    };
};
