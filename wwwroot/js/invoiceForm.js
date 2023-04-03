class UpdateStatusSse {

    execute() {
        let self = this;
        const evtSource = new EventSource("/stream-sse");
        evtSource.addEventListener('invoice/update', event => {
            let data = JSON.parse(event.data);
            addInvoiceSnackBar(data);
        })

        evtSource.onerror = function(e) {
            evtSource.close();
            self.execute();
        };
    }
}