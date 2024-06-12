const event = new Event("tableContentChanged");

(function (component) {

    const emptyCell = {
        Value: '',
        Alignment: 'start',
        ColSpan: '1',
        RowSpan: '1'
    };

    const emptyRow = {
        NoBorder: false,
        Cells: []
    }

    component.init = function (id) {
        console.log('Tables initializer');
        getEditorsInstances(id);
    }

    function getTableObject(container) {
        return JSON.parse(container.find('[data-property-name="tableContent"] textarea').val());
    }

    function getCollectionName(isHeader) {
        return isHeader ? 'Headers' : 'Rows';
    }

    function getElementType(isHeader) {
        return isHeader ? 'th' : 'td';
    }

    function getCellValue(container, rowIndex, colIndex, propertyName, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        return obj[collectionName][rowIndex].Cells[colIndex][propertyName];
    }

    function setCellValue(container, rowIndex, colIndex, propertyName, value, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        obj[collectionName][rowIndex].Cells[colIndex][propertyName] = value;

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));
        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function getAlignmentValue(container, rowIndex, colIndex, isHeader) {
        return getCellValue(container, rowIndex, colIndex, 'Alignment', isHeader);
    }

    function setAlignmentValue(container, rowIndex, colIndex, value, isHeader) {
        setCellValue(container, rowIndex, colIndex, 'Alignment', value, isHeader);
    }

    function getBorderValue(container, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        return obj[collectionName][rowIndex].Cells[colIndex].NoBorder ?? false;
    }

    function setBorderValue(container, rowIndex, colIndex, value, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        obj[collectionName][rowIndex].Cells[colIndex].NoBorder = value;

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));
        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function mergeColumnToRight(container, cellElement, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        const elementType = getElementType(isHeader);

        const currentCellColSpan = parseInt(obj[collectionName][rowIndex].Cells[colIndex]['ColSpan']);
        const nextCellColSpan = parseInt(obj[collectionName][rowIndex].Cells[colIndex + 1]['ColSpan']);

        obj[collectionName][rowIndex].Cells.splice(colIndex + 1, 1);

        obj[collectionName][rowIndex].Cells[colIndex]['ColSpan'] = (currentCellColSpan + nextCellColSpan).toString();

        cellElement.attr('colspan', currentCellColSpan + nextCellColSpan);
        cellElement.next(elementType).remove();

        cellElement.nextAll(elementType).each((index, element) => {
            $(element).attr('data-col-index', colIndex + 1 + index);
        });

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));

        initCellsWithValues(container);

        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function unmergeColumnToRight(container, cellElement, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        const elementType = getElementType(isHeader);

        const currentCellColSpan = parseInt(obj[collectionName][rowIndex].Cells[colIndex]['ColSpan']);
        obj[collectionName][rowIndex].Cells[colIndex]['ColSpan'] = '1';

        const numberOfColumnsToAdd = currentCellColSpan - 1;

        cellElement.removeAttr('colspan');

        for (let colToAddIndex = 0; colToAddIndex < numberOfColumnsToAdd; colToAddIndex++) {

            obj[collectionName][rowIndex].Cells.splice(colIndex + colToAddIndex + 1, 0, JSON.parse(JSON.stringify(emptyCell)));

            const clonedCellElement = cellElement.clone();

            if (isHeader) {
                clonedCellElement.find('input[data-property-name="Value"]').val('');
            }
            else {
                const rtEditorElement = clonedCellElement.find('div[data-inline-editor="Kentico.InlineEditor.RichText"]');

                rtEditorElement.after('<div>Salvare per poter modificare il contenuto.</div>');
                rtEditorElement.remove();
            }
            clonedCellElement.appendTo(cellElement.parent());
        }

        cellElement.nextAll(elementType).each((index, element) => {
            $(element).attr('data-col-index', colIndex + 1 + index);
        });

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));

        initCellsWithValues(container);

        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function mergeRowToBottom(container, cellElement, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        const elementType = getElementType(isHeader);

        const currentCellRowSpan = parseInt(obj[collectionName][rowIndex].Cells[colIndex]['RowSpan']);

        const nextCellColIndex = Math.min(colIndex, obj[collectionName][rowIndex + currentCellRowSpan].Cells.length - 1);

        const nextCellRowSpan = parseInt(obj[collectionName][rowIndex + currentCellRowSpan].Cells[nextCellColIndex]['RowSpan']);

        obj[collectionName][rowIndex + currentCellRowSpan].Cells.splice(nextCellColIndex, 1);

        obj[collectionName][rowIndex].Cells[colIndex]['RowSpan'] = (currentCellRowSpan + nextCellRowSpan).toString();

        cellElement.attr('rowspan', currentCellRowSpan + nextCellRowSpan);
        const elementToRemove = cellElement.parent().parent().find(`${elementType}[data-row-index="${rowIndex + currentCellRowSpan}"][data-col-index="${nextCellColIndex}"]`);

        elementToRemove.nextAll(elementType).each((index, element) => {
            $(element).attr('data-col-index', nextCellColIndex + index);
        });

        elementToRemove.remove();

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));

        initCellsWithValues(container);

        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function unmergeRowToBottom(container, cellElement, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);
        const elementType = getElementType(isHeader);

        const currentCellRowSpan = parseInt(obj[collectionName][rowIndex].Cells[colIndex]['RowSpan']);
        obj[collectionName][rowIndex].Cells[colIndex]['RowSpan'] = '1';

        const numberOfRowsToAdd = currentCellRowSpan - 1;

        cellElement.removeAttr('rowspan');

        for (let rowToAddIndex = 0; rowToAddIndex < numberOfRowsToAdd; rowToAddIndex++) {

            const nextCellColIndex = Math.min(colIndex, obj[collectionName][rowIndex + 1].Cells.length);

            obj[collectionName][rowToAddIndex + 1].Cells.splice(nextCellColIndex, 0, JSON.parse(JSON.stringify(emptyCell)));

            const clonedCellElement = cellElement.clone();

            if (isHeader) {
                clonedCellElement.find('input[data-property-name="Value"]').val('');
            }
            else {
                const rtEditorElement = clonedCellElement.find('div[data-inline-editor="Kentico.InlineEditor.RichText"]');

                rtEditorElement.after('<div>Salvare per poter modificare il contenuto.</div>');
                rtEditorElement.remove();
            }

            clonedCellElement.attr('data-row-index', rowToAddIndex + 1);
            clonedCellElement.attr('data-col-index', nextCellColIndex);

            if (nextCellColIndex === 0) {
                clonedCellElement.prependTo(
                    cellElement.parent().parent().find(`tr:nth-child(${rowToAddIndex + 2})`)
                );
            } else {
                clonedCellElement.insertAfter(
                    cellElement.parent().parent().find(`tr:nth-child(${rowToAddIndex + 2})`).find(`${elementType}:nth-child(${nextCellColIndex})`)
                );
            }

            clonedCellElement.nextAll(elementType).each((index, element) => {
                $(element).attr('data-col-index', nextCellColIndex + 1 + index);
            });
        }

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));

        initCellsWithValues(container);

        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function addRow(container, cellElement, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        obj[collectionName].push(JSON.parse(JSON.stringify(emptyRow)));

        const numberOfColumnsToAdd = obj[collectionName][rowIndex].Cells.length;

        const newRowElement = $('<tr>');

        for (let colToAddIndex = 0; colToAddIndex < numberOfColumnsToAdd; colToAddIndex++) {

            obj[collectionName][rowIndex + 1].Cells.splice(colToAddIndex, 0, JSON.parse(JSON.stringify(emptyCell)));

            const clonedCellElement = cellElement.clone();
            clonedCellElement.attr('data-col-index', colToAddIndex);
            clonedCellElement.attr('data-row-index', rowIndex + 1);

            if (isHeader) {
                clonedCellElement.find('input[data-property-name="Value"]').val('');
            }
            else {
                const rtEditorElement = clonedCellElement.find('div[data-inline-editor="Kentico.InlineEditor.RichText"]');

                rtEditorElement.after('<div>Salvare per poter modificare il contenuto.</div>');
                rtEditorElement.remove();
            }
            clonedCellElement.appendTo(newRowElement);
        }

        newRowElement.appendTo(cellElement.parent().parent());

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));

        initCellsWithValues(container);

        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function removeRow(container, cellElement, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        obj[collectionName].pop();

        cellElement.parent().remove();

        container.find('[data-property-name="tableContent"] textarea').val(JSON.stringify(obj));

        initCellsWithValues(container);

        container.find('[data-property-name="tableContent"] textarea')[0].dispatchEvent(event);
    }

    function toggleMergeColumnButtonVisibility(container, mergeButton, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        let showButton =
            colIndex < obj[collectionName][rowIndex].Cells.length - 1 && obj[collectionName][rowIndex].Cells[colIndex].RowSpan == '1';

        if (colIndex + 1 < obj[collectionName][rowIndex].Cells.length && obj[collectionName][rowIndex].Cells[colIndex + 1].RowSpan != '1') {
            showButton = false;
        }

        mergeButton.toggle(showButton);
    }

    function toggleUnmergeColumnButtonVisibility(container, unmergeButton, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        const showButton = obj[collectionName][rowIndex].Cells[colIndex].ColSpan != "1";

        unmergeButton.toggle(showButton);
    }

    function toggleMergeRowButtonVisibility(container, mergeButton, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        let showButton =
            parseInt(obj[collectionName][rowIndex].Cells[colIndex].RowSpan) < obj[collectionName].length &&
            rowIndex < obj[collectionName].length - 1 &&
            obj[collectionName][rowIndex].Cells[colIndex].ColSpan == '1';

        if (rowIndex + 1 < obj[collectionName].length && colIndex >= obj[collectionName][rowIndex + 1].Cells.length) {
            showButton = false;
        }

        else if (rowIndex + 1 < obj[collectionName].length && obj[collectionName][rowIndex + 1].Cells[colIndex].ColSpan != '1') {
            showButton = false;
        }

        mergeButton.toggle(showButton);
    }

    function toggleUnmergeRowButtonVisibility(container, unmergeButton, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        const showButton = obj[collectionName][rowIndex].Cells[colIndex].RowSpan != "1";

        unmergeButton.toggle(showButton);
    }

    function toggleAddRowButtonVisibility(container, addRowButton, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        const showButton = colIndex === 0 && rowIndex === obj[collectionName].length - 1;
        addRowButton.toggle(showButton);
    }

    function toggleRemoveRowButtonVisibility(container, removeRowButton, rowIndex, colIndex, isHeader) {
        const obj = getTableObject(container);
        const collectionName = getCollectionName(isHeader);

        const showButton = colIndex === 0 && rowIndex === obj[collectionName].length - 1 && obj[collectionName].length > 1;
        removeRowButton.toggle(showButton);
    }

    function initCellsWithValues(container) {

        //Esamino tutte le celle (th e tr)
        container.find('th,td').each(function () {
            const cellElement = $(this);
            const isHeader = cellElement.is('th');

            const rowIndex = parseInt(cellElement.attr('data-row-index'));
            const colIndex = parseInt(cellElement.attr('data-col-index'));

            //--- Controlli allinamenti ---
            const alignment = getAlignmentValue(container, rowIndex, colIndex, isHeader);

            cellElement.find('a.alignment').removeClass('active');
            cellElement.find('a.alignment[data-value="' + alignment + '"]').addClass('active');

            cellElement.find('a.alignment').each(function () {
                const alignmentLink = $(this);
                const value = alignmentLink.attr('data-value');
                alignmentLink.off('click').on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    cellElement.find('a.alignment').removeClass('active');
                    setAlignmentValue(container, rowIndex, colIndex, value, isHeader);
                    alignmentLink.addClass('active');
                });
            });

            //--- Controlli merge colonne ---
            cellElement.find('a.merge-col').each(function () {
                const mergeButton = $(this);

                toggleMergeColumnButtonVisibility(container, mergeButton, rowIndex, colIndex, isHeader);

                mergeButton.off('click').on('click', function () {
                    mergeColumnToRight(container, cellElement, rowIndex, colIndex, isHeader);
                });
            });

            //--- Controlli unmerge colonne ---
            cellElement.find('a.unmerge-col').each(function () {
                const unmergeButton = $(this);

                toggleUnmergeColumnButtonVisibility(container, unmergeButton, rowIndex, colIndex, isHeader);

                unmergeButton.off('click').on('click', function () {
                    unmergeColumnToRight(container, cellElement, rowIndex, colIndex, isHeader);
                });
            });

            //--- Controlli merge righe ---
            cellElement.find('a.merge-row').each(function () {
                const mergeButton = $(this);

                toggleMergeRowButtonVisibility(container, mergeButton, rowIndex, colIndex, isHeader);

                mergeButton.off('click').on('click', function () {
                    mergeRowToBottom(container, cellElement, rowIndex, colIndex, isHeader);
                });
            });

            //--- Controlli unmerge righe ---
            cellElement.find('a.unmerge-row').each(function () {
                const unmergeButton = $(this);

                toggleUnmergeRowButtonVisibility(container, unmergeButton, rowIndex, colIndex, isHeader);

                unmergeButton.off('click').on('click', function () {
                    unmergeRowToBottom(container, cellElement, rowIndex, colIndex, isHeader);
                });
            });

            //--- Controlli aggiunta righe ---
            cellElement.find('a.add-row').each(function () {
                const addRowButton = $(this);

                toggleAddRowButtonVisibility(container, addRowButton, rowIndex, colIndex, isHeader);

                addRowButton.off('click').on('click', function () {
                    addRow(container, cellElement, rowIndex, colIndex, isHeader);
                });
            });

            //--- Controlli rimozione righe ---
            cellElement.find('a.remove-row').each(function () {
                const removeRowButton = $(this);

                toggleRemoveRowButtonVisibility(container, removeRowButton, rowIndex, colIndex, isHeader);

                removeRowButton.off('click').on('click', function () {
                    removeRow(container, cellElement, rowIndex, colIndex, isHeader);
                });
            });

            //--- Controlli valori header ---
            if (isHeader) {
                cellElement.find('input[type="text"]').each(function () {
                    const input = $(this);

                    const propertyName = input.attr('data-property-name');
                    input.val(getCellValue(container, rowIndex, colIndex, propertyName, true));

                    input.off('blur').on('blur', function () {
                        setCellValue(container, rowIndex, colIndex, propertyName, input.val(), true);
                    });
                });
            }

            //--- Controlli per rimuovere bordi righe ---
            const pulsanteBordo = cellElement.find('a.pulsante-caratteristiche.border');
            const border = getBorderValue(container, rowIndex, colIndex, isHeader);

            pulsanteBordo.removeClass('active');

            if (border) {
                pulsanteBordo.addClass('active');
            }

            pulsanteBordo.off('click').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();

                const value = !pulsanteBordo.hasClass('active');

                setBorderValue(container, rowIndex, colIndex, value, isHeader);

                if (pulsanteBordo.hasClass('active')) {
                    pulsanteBordo.removeClass('active');
                }
                else {
                    pulsanteBordo.addClass('active');
                }
            });

        });
    }

    function loadEditors(container) {
        let editorsLoaded = false;
        container.find('.ktc-rich-text-wrapper').each(function () {
            const self = $(this);
            if (self[0]['data-froala.editor']) {

                const editor = self[0]['data-froala.editor'];

                const rowIndex = parseInt(self.closest('td').attr('data-row-index'));
                const colIndex = parseInt(self.closest('td').attr('data-col-index'));

                editor.html.set(getCellValue(container, rowIndex, colIndex, 'Value', false));

                editor.events.on('blur', function () {
                    const html = $(this.html.get(false));
                    html.find('.fr-marker').each(function () {
                        $(this).detach();
                    });
                    var value = '';
                    for (var j = 0; j < html.length; j++)
                      value += html[j].outerHTML;
                    setCellValue(container, rowIndex, colIndex, 'Value', value, false);
                });

                editorsLoaded = true;
            };
        });

        return editorsLoaded;
    }

    function getEditorsInstances(id) {
        const container = $('.tables-container[data-id="' + id + '"]');
        if (container.attr('data-editors-loaded') == 'false') {
            let editorsLoaded = false;
            if ($('.tables-container .ktc-rich-text-wrapper').length > 0) {

                editorsLoaded = loadEditors(container);
                if (editorsLoaded) {
                    container.attr('data-editors-loaded', true);
                }
            }

            if (!editorsLoaded) {
                setTimeout(function () { getEditorsInstances(id); }, 200);
            }
            else {
                container.find('[data-id="content-toggler"]').on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    container.find('textarea').toggle();
                });

                initCellsWithValues(container);
            }
        }
    }
}(window.$eloTablesBe = window.$eloTablesBe || {}));

(function () {
    // Registers the 'number-editor' inline property editor within the page builder scripts
    window.kentico.pageBuilder.registerInlineEditor("table-content-editor", {
        init: function (options) {
            var editor = options.editor;

            //recupero l'id dell'editor
            var guid = editor.getAttribute("guid");

            //mi abbono all'evento del keyup
            editor.querySelector("#ta-" + guid).addEventListener("tableContentChanged", function () {
                console.log('property changed')
                // Creates a custom event that notifies the widget about a change in the value of a property
                var event = new CustomEvent("updateProperty", {
                    detail: {
                        value: editor.querySelector("#ta-" + guid).value,
                        name: options.propertyName,
                        refreshMarkup: false
                    }
                });
                console.log(event)
                editor.dispatchEvent(event);
            });
        }
    });
})();