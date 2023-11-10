# CheckVatAndCountryCode
CheckVatAndCountryCode is a REST web service that checks if a VAT number, its country code and its country description are correct.

## Usage

First of all, compile the solution using CTRL+F5 on Visual Studio

When swagger is opened, clic on "try it out" and then "execute". Now you can copy the URL generated and paste it on postman.

``` url
https://localhost:port/APIWeb
```

Add 'devName' and 'team' fields in 'header' menu.

Add a JSON body with this format:
``` json
{
  "countryCode": "string",
  "vatNumber": "string",
  "countryDescription": "string"
}
```
Last but not least, send the request and format the JSON response received.

## License
[GPL 3.0](https://www.gnu.org/licenses/gpl-3.0.html)
