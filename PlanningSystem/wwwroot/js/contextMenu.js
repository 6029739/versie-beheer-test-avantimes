// Context menu functionality
document.addEventListener('DOMContentLoaded', function() {
    // Create context menu element
    const contextMenu = document.createElement('div');
    contextMenu.className = 'context-menu';
    contextMenu.style.display = 'none';
    document.body.appendChild(contextMenu);

    // Add styles for context menu
    const style = document.createElement('style');
    style.textContent = `
        .context-menu {
            position: fixed;
            background: white;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-shadow: 2px 2px 5px rgba(0,0,0,0.2);
            padding: 5px 0;
            z-index: 1000;
        }
        .context-menu-item {
            padding: 8px 15px;
            cursor: pointer;
            white-space: nowrap;
        }
        .context-menu-item:hover {
            background-color: #f0f0f0;
        }
        .context-menu-separator {
            height: 1px;
            background-color: #ccc;
            margin: 5px 0;
        }
    `;
    document.head.appendChild(style);

    // Handle right-click on function and service buttons
    document.querySelectorAll('.function-button, .service-button').forEach(button => {
        button.addEventListener('contextmenu', function(e) {
            e.preventDefault();
            
            const isFunction = this.classList.contains('function-button');
            const type = isFunction ? 'function' : 'service';
            const id = isFunction ? this.dataset.functionId : this.dataset.shiftId;
            const name = isFunction ? this.dataset.functionName : this.dataset.shiftName;
            const icon = isFunction ? this.dataset.functionIcon : this.dataset.shiftIcon;

            // Create context menu items
            contextMenu.innerHTML = `
                <div class="context-menu-item" data-action="rename" data-type="${type}" data-id="${id}">Naam wijzigen</div>
                <div class="context-menu-item" data-action="add" data-type="${type}">Nieuwe ${isFunction ? 'functie' : 'dienst'} toevoegen</div>
            `;

            // Position the context menu
            contextMenu.style.display = 'block';
            contextMenu.style.left = e.pageX + 'px';
            contextMenu.style.top = e.pageY + 'px';

            // Handle context menu item clicks
            contextMenu.querySelectorAll('.context-menu-item').forEach(item => {
                item.addEventListener('click', async function() {
                    const action = this.dataset.action;
                    const itemType = this.dataset.type;

                    if (action === 'rename') {
                        const newName = prompt('Nieuwe naam:', name);
                        if (newName && newName.trim()) {
                            try {
                                const response = await fetch(`/api/${itemType}s/${id}`, {
                                    method: 'PUT',
                                    headers: {
                                        'Content-Type': 'application/json',
                                    },
                                    body: JSON.stringify({
                                        id: id,
                                        name: newName,
                                        icon: icon,
                                        color: '#007bff' // Default color
                                    })
                                });

                                if (response.ok) {
                                    // Update the button text
                                    button.textContent = newName;
                                    if (isFunction) {
                                        button.dataset.functionName = newName;
                                    } else {
                                        button.dataset.shiftName = newName;
                                    }
                                } else {
                                    alert('Er is een fout opgetreden bij het wijzigen van de naam.');
                                }
                            } catch (error) {
                                console.error('Error:', error);
                                alert('Er is een fout opgetreden bij het wijzigen van de naam.');
                            }
                        }
                    } else if (action === 'add') {
                        const newName = prompt(`Nieuwe ${itemType === 'function' ? 'functie' : 'dienst'} naam:`, '');
                        if (newName && newName.trim()) {
                            try {
                                const response = await fetch(`/api/${itemType}s`, {
                                    method: 'POST',
                                    headers: {
                                        'Content-Type': 'application/json',
                                    },
                                    body: JSON.stringify({
                                        name: newName,
                                        icon: '📝', // Default icon
                                        color: '#007bff' // Default color
                                    })
                                });

                                if (response.ok) {
                                    const newItem = await response.json();
                                    // Create new button
                                    const newButton = document.createElement('a');
                                    newButton.href = '#';
                                    newButton.className = `${itemType}-button`;
                                    newButton.draggable = true;
                                    newButton.dataset[`${itemType}Id`] = newItem.id;
                                    newButton.dataset[`${itemType}Name`] = newItem.name;
                                    newButton.dataset[`${itemType}Icon`] = newItem.icon;
                                    newButton.innerHTML = `
                                        <span class="button-icon">${newItem.icon}</span>
                                        ${newItem.name}
                                    `;

                                    // Add drag and drop functionality
                                    newButton.addEventListener('dragstart', function(e) {
                                        e.dataTransfer.setData('text/plain', JSON.stringify({
                                            type: itemType,
                                            id: newItem.id,
                                            name: newItem.name,
                                            icon: newItem.icon
                                        }));
                                        this.classList.add('dragging');
                                    });

                                    newButton.addEventListener('dragend', function() {
                                        this.classList.remove('dragging');
                                    });

                                    // Add to the appropriate section
                                    const section = document.querySelector(`.sidebar-section:has(.${itemType}-button)`);
                                    section.appendChild(newButton);
                                } else {
                                    alert('Er is een fout opgetreden bij het toevoegen van de nieuwe item.');
                                }
                            } catch (error) {
                                console.error('Error:', error);
                                alert('Er is een fout opgetreden bij het toevoegen van de nieuwe item.');
                            }
                        }
                    }
                    contextMenu.style.display = 'none';
                });
            });
        });
    });

    // Hide context menu when clicking outside
    document.addEventListener('click', function(e) {
        if (!contextMenu.contains(e.target)) {
            contextMenu.style.display = 'none';
        }
    });
}); 