function HandleBackFunctionality() {
    if (window.event) {
        if (window.event.clientX < 40 && window.event.clientY < 0) {
            location.reload();
        }
    }
    else {
        if (performance.navigation.type == 2) {
            location.reload();
        }
    }
}
HandleBackFunctionality();