let flashingRows = [];
    function toggleRowSelection(element) {
        const isSelected = flashingRows.includes(element);
        const isLastRow = !element.nextElementSibling; 
        if (isSelected) {
            flashingRows = flashingRows.filter(row => row !== element);
            element.classList.remove("selected-row");
            element.classList.remove("has-bottom-border");
            const previousRow = element.previousElementSibling;
            if (previousRow) {
                if (!flashingRows.includes(previousRow)) {
                    previousRow.classList.remove("has-bottom-border");
                }
            }
        } else {
            flashingRows.push(element);
            element.classList.add("selected-row");
            if (!isLastRow) {
                element.classList.add("has-bottom-border");
            }
            const previousRow = element.previousElementSibling;
            if (previousRow && !flashingRows.includes(previousRow)) {
                previousRow.classList.add("has-bottom-border");
            }
        }
    }