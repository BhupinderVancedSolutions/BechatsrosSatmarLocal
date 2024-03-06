class CustomSeasonTypeFilter {
    init(params) {
        this.params = params;
        this.filterText = null;
        this.setupGui(params);
    }

    // not called by AG Grid, just for us to help setup
    setupGui(params) {
        this.gui = document.createElement('div');
        this.gui.innerHTML = `<select id='SeasonTypeId' style='width:100%;height:25px;'>
                 <option value="0">Select Season Type</option>
                  <option value="1">First Half</option>
                  <option value="2">Second Half</option>
                  <option value="3">Entire Season</option>
                </select>`;

        const listener = (event) => {
            this.filterText = event.target.value;            
            params.filterChangedCallback();
        };

        this.eFilterText = this.gui.querySelector('#SeasonTypeId');
        this.eFilterText.addEventListener('changed', listener);
        this.eFilterText.addEventListener('paste', listener);
        this.eFilterText.addEventListener('input', listener);
    }

    getGui() {
        return this.gui;
    }

    doesFilterPass(params) {
        const { api, colDef, column, columnApi, context } = this.params;
        const { node } = params;

        // make sure each word passes separately, ie search for firstname, lastname
        let passed = true;
        _optionalChain([
            this,
            'access',
            (_) => _.filterText,
            'optionalAccess',
            (_2) => _2.toLowerCase,
            'call',
            (_3) => _3(),
            'access',
            (_4) => _4.split,
            'call',
            (_5) => _5(' '),
            'access',
            (_6) => _6.forEach,
            'call',
            (_7) =>
                _7((filterWord) => {
                    const value = this.params.valueGetter({
                        api,
                        colDef,
                        column,
                        columnApi,
                        context,
                        data: node.data,
                        getValue: (field) => node.data[field],
                        node,
                    });

                    if (value.toString().toLowerCase().indexOf(filterWord) < 0) {
                        passed = false;
                    }
                }),
        ]);

        return passed;
    }

    isFilterActive() {
        return this.filterText != null && this.filterText !== '';
    }

    getModel() {
        if (!this.isFilterActive()) {
            return null;
        }

        return { value: this.filterText };
    }

    setModel(model) {
        this.eFilterText.value = model == null ? null : model.value;
    }
}