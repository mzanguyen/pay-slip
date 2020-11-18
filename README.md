# MYOB Codding Challenge

Create a console application, given employee details, output monthly payslip
Tax calculation algorithm is in /MyobCodingChallenge.Payslip/MyobCodingChallenge.Payslip.Service/Services/TaxService.cs

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.


### Assumptions

+ No authentication required. Tax band data is not confidential
+ Main purpose of this challenge is to showcase coding skill. Based on this assumption, proper library dependancy injection, Ioption, library project etc... were setup even if the problem itself does not need this big of an architecture
+ The solution is written with S.O.L.I.D princliples in mind => some code may seem like an overkill for a simple purpose of calculating tax. However, with this setup, different tax rate can be applied without changing the code
+ No integration test is written but unit tests for all calculation logic are included

### Tech debt

+ Set up proper logging mechanism
+ Add comments to explain the calculation
+ Set up Exception handler middleware and setup/ teardown messages
+ This code is not production ready
+ Integration test(s)
+ Put all magic strings and magic integers in a constant file 

### Prerequisites

Visual studio 2017/2019 and .net core 3.1

## Deployment

For the sake of simplicity, this solution is not deployed

