# TaxServiceAPI
A service built to retrieve tax data from external APIs and calculate tax owed. I went a bit beyond the basic spec to make the service more complete and extensible.

## Design
### TaxService
Calls on **ITaxCalculator**s to determine taxes to collect. Decides which implementation to use based on Customer and retrieves it from **CalculatorFactory**.

### TaxJarCalculator
Gets data from the [TaxJarAPI](https://developers.taxjar.com/api/reference/) to calculate taxes. I decided to simply return the aggregate tax values from each API result set as I didn't see a need for further processing.

### Testing
Unit tests for all methods in **TaxService** and **TaxJarCalculator** are included in **TaxServiceAPITests**. I also included some quick integration tests that I used to check that the service could connect to the TaxJarAPI.
