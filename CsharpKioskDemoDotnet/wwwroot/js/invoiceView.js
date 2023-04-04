class UpdateStatusSse {
    invoiceId;

    constructor(invoiceId) {
        this.invoiceId = invoiceId;
    }

    execute() {
        let self = this;
        const evtSource = new EventSource("/stream-sse");
        evtSource.addEventListener('invoice/update', event => {
            let data = JSON.parse(event.data);

            addInvoiceSnackBar(data);
            if (data.invoiceId !== this.invoiceId) {
                return;
            }
            
            let statusTextItem = document.getElementsByClassName("status")[0];

            if (!statusTextItem) {
                return;
            }

            statusTextItem.classList.replace("grid-status-" + statusTextItem.textContent, "grid-status-" + data.status)
            statusTextItem.textContent = data.status;
        })

        evtSource.onerror = function(e) {
            evtSource.close();
            self.execute();
        };
    }
}
