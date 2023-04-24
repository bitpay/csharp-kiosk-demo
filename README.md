# BitPay Kiosk Demo - C# / .NET

This is a demonstration .NET app to show how BitPay can be used in the
context of a retail kiosk. It utilizes the `pos` facade and with a simple
configuration file you can customize the `posData` fields that are sent to
BitPay. This app uses SQLite database to make it easy to start.

## Functionality

- Create invoices
- View a grid of invoices
- View invoice details
- Store invoices in a database
- Receives instant payment notifications (IPN) to update the database
- Uses EventSource to update the frontend upon receiving IPN

## Prerequisites

- BitPay Account
- .NET SDK >= 7

## Configuration

This app uses a JSON and YAML configuration files to set BitPay credentials and form fields. 
To configure it, you'll need to update `bitPayDesign.yaml`.

### General Information

#### appsettings.json

| JSON Key                                | Description                                             |
|-----------------------------------------| ------------------------------------------------------- |
| AppUrl                                  | Sets the application URL used for IPN                   |
| BitPay.Token                            | Your BitPay token                                       |
| BitPay.NotificationEmail                | The email you want to use for notifications             |

#### bitPayDesign.yaml

| YAML Key                                | Description                                             |
|-----------------------------------------| ------------------------------------------------------- |
| hero.bgColor                            | CSS color for hero background                           |
| hero.title                              | The title to show in the hero                           |
| hero.body                               | The text to show under the title in the hero            |
| logo                                    | URL for the logo                                        |
| posData.fields                          | See the `POS Data Fields` section below                 |

### POS Data Fields

#### Dropdown (posData)

| YAML Key                                | Description                                            |
|-----------------------------------------| ------------------------------------------------------ |
| type                                    | Set to "select"                                        |
| required                                | Determines whether the field should be required or not |
| id                                      | Field identifier                                       |
| name                                    | Field name                                             |
| label                                   | Field label                                            |
| options.id                              | (options array) ID for a given selection               |
| options.label                           | (options array) Label for a given selection            |
| options.value                           | (options array) Value for a given selection            |

#### Fieldset (posData)

| YAML Key                                | Description                                            |
|-----------------------------------------| ------------------------------------------------------ |
| type                                    | Set to "fieldset"                                      |
| required                                | Determines whether the field should be required or not |
| name                                    | Field name                                             |
| label                                   | Field label                                            |
| options.id                              | (options array) ID for a given selection               |
| options.label                           | (options array) Label for a given selection            |
| options.value                           | (options array) Value for a given selection            |

#### Text (posData)

| YAML Key                                | Description                                            |
|-----------------------------------------| ------------------------------------------------------ |
| type                                    | Set to "text"                                          |
| required                                | Determines whether the field should be required or not |
| name                                    | Field name                                             |
| label                                   | Field label                                            |

#### Price

| YAML Key                                | Description                                            |
|-----------------------------------------| ------------------------------------------------------ |
| type                                    | Set to "price"                                         |
| required                                | Determines whether the field should be required or not |
| name                                    | Field name                                             |
| label                                   | Field label                                            |
| currency                                | Currency for the field                                 |

Field with name "price" is required to exists in form. 
If field is not added to file `bitPayDesign.yaml`, it will be added automatically to form when the application starts.

## Running

Run `dotnet run --project CsharpKioskDemoDotnet` to run the application.

## Testing

Run `dotnet test` to run tests.