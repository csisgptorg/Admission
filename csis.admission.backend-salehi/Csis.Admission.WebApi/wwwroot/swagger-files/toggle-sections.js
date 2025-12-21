function addExpandCollapseButtons() {
    // Create Expand All button
    const expandAllButton = document.createElement("button");
    expandAllButton.innerHTML = '<span>Expand All</span><svg width="20" height="20"><use href="#large-arrow-down" xlink:href="#large-arrow-down"></use></svg>'
    expandAllButton.classList.add('btn', 'expand-all');
    expandAllButton.style.opacity = 0;
    expandAllButton.addEventListener("click", function () {
        expandSections();
    });

    // Create Collapse All button
    const collapseAllButton = document.createElement("button");
    collapseAllButton.innerHTML = '<span>Collapse All</span><svg width="20" height="20"><use href="#large-arrow-up" xlink:href="#large-arrow-up"></use></svg>'
    collapseAllButton.classList.add('btn', 'collapse-all');
    collapseAllButton.style.opacity = 0;
    collapseAllButton.addEventListener("click", function () {
        collapseSections();
    });

    // Add buttons to Swagger UI
    const authWrapper = document.querySelector('.scheme-container .auth-wrapper');

    if (authWrapper) {
        authWrapper.appendChild(expandAllButton);
        authWrapper.appendChild(collapseAllButton);
    } else {
        const sectionsWrapper = document.querySelectorAll('.swagger-container .swagger-ui div.wrapper:not(.information-container)')[0];
        if (sectionsWrapper) {
            const buttonsContainer = document.createElement("div");
            buttonsContainer.classList.add('wrapper');
            buttonsContainer.style.direction = 'rtl';

            buttonsContainer.appendChild(expandAllButton);
            buttonsContainer.appendChild(collapseAllButton);
            sectionsWrapper.parentNode.insertBefore(buttonsContainer, sectionsWrapper);
        }
    }

    setTimeout(function () {
        expandAllButton.style.opacity = 1;
        collapseAllButton.style.opacity = 1;
    }, 250);

}

// Expand all sections in Swagger UI
function expandSections() {
    const closedSections = document.querySelectorAll('h3.opblock-tag[data-is-open="false"]');
    for (let i = 0; i < closedSections.length; i++) {
        closedSections[i].click();
    }
}

// Collapse all sections in Swagger UI
function collapseSections() {
    const openSections = document.querySelectorAll('h3.opblock-tag[data-is-open="true"]');
    for (let i = 0; i < openSections.length; i++) {
        openSections[i].click();
    }
}

// Wait for Swagger UI to load and add buttons
document.addEventListener('DOMContentLoaded', function () {
    setTimeout(addExpandCollapseButtons, 3000);
})