function addThemeToggleButton() {
    const label = document.createElement('label');
    label.setAttribute('for', 'theme');
    label.classList.add('theme');

    // Create the first span element
    const span1 = document.createElement('span');
    span1.textContent = 'Light';

    // Create the second span element (theme__toggle-wrap)
    const span2 = document.createElement('span');
    span2.classList.add('theme__toggle-wrap');

    // Create the input element
    const input = document.createElement('input');
    input.setAttribute('type', 'checkbox');
    input.classList.add('theme__toggle');
    input.id = 'theme';
    input.setAttribute('role', 'switch');
    input.setAttribute('name', 'theme');
    input.setAttribute('value', 'dark');
    input.onchange = function () {
        if (input.checked) {
            document.body.classList.add('dark');
            localStorage.setItem('swagger-theme', 'dark');
        } else {
            document.body.classList.remove('dark');
            localStorage.setItem('swagger-theme', 'light');
        }
    };

    // Create the third span element (theme__fill)
    const span3 = document.createElement('span');
    span3.classList.add('theme__fill');

    // Create the fourth span element (theme__icon)
    const span4 = document.createElement('span');
    span4.classList.add('theme__icon');

    // Create the nine span elements (theme__icon-part) inside span4
    for (let i = 0; i < 9; i++) {
        const spanPart = document.createElement('span');
        spanPart.classList.add('theme__icon-part');
        span4.appendChild(spanPart);
    }

    // Append all the elements to their respective parent elements
    span2.appendChild(input);
    span2.appendChild(span3);
    span2.appendChild(span4);

    //label.appendChild(span1);
    label.appendChild(span2);

    // Create the last span element
    const span5 = document.createElement('span');
    span5.textContent = 'Dark';

    //label.appendChild(span5);
    const container = document.querySelector('#swagger-ui .topbar .wrapper .topbar-wrapper');
    container.appendChild(label);
}

// Wait for Swagger UI to load and add buttons
document.addEventListener('DOMContentLoaded', function () {
    setTimeout(function () {
        addThemeToggleButton();
        if ((localStorage.getItem('swagger-theme') || '') == 'dark') {
            document.body.classList.add('dark');
            document.getElementById('theme').checked = true
        }
    }, 1000);
})