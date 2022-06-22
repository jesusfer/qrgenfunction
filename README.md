# QR Generator

This is an Azure Function project used to generate a QR Code.

It only has one endpoint `/api/qrgen`.
The data to encode is passed using the `data` query string parameter.

Example:
```
/api/qrgen?data=hi,%20there
```
