class Toast {
    constructor(message, color, time) {
        this.message = message;
        this.color = color;
        this.time = time;
        this.element = null;
        var element = document.createElement('div');
        element.className = "toast-notification";
        this.element = element;
        var countElements = document.getElementsByClassName("toast-notification");

        element.style.opacity = 0;

        element.style.marginBottom = (countElements.length * 55) + "px";

        element.style.backgroundColor = this.color;

        var message = document.createElement("div");
        message.className = "message-container";
        message.textContent = this.message;

        element.appendChild(message);

        var close = document.createElement("div");
        close.className = "close-notification";

        const icon = document.createElement("span");
        icon.innerHTML = '<svg width="20" height="20"><use href="#close" xlink:href="#close"></use></svg>';

        close.appendChild(icon);

        element.append(close);

        document.body.appendChild(element);

        setTimeout(function () {
            element.style.opacity = 0.85;
        }, 100);

        setTimeout(function () {
            element.style.opacity = 0;
            setTimeout(function () {
                element.remove();
            }, 1000);
        }, this.time);

        close.addEventListener("click", () => {
            element.style.opacity = 0;
            setTimeout(function () {
                element.remove();
            }, 1000);
        })
    }

}

const ToastType = {
    Danger: "#eb3b5a",
    Warning: "#fdcb6e",
    Success: "#00b894",
}