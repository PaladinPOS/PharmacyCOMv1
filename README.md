PharmacyCOMv1
=============
# ABOUT
Paladin Data Corporation is providing a way for pharmacy systems to communicate with the Paladin POS system. This service enables pharmacy systems to utilize the Paladin POS GUI front end while retaining their current system and business function. 

Pharmacy systems just need to implement a simple SOAP-based webservice in order to allow Paladin POS to communicate with your system. 

PharmacyCOMv1 provides a WSDL (web service definition language) as well as a base .NET Webservice as a starting point for implementing this service.

# WEBSERVICE METHODS
The webservice should provide the following methods to Paladin POS:
* GetRxItem(rxNumber) - Allows Paladin POS to query the pharmacy system for a specific Rx numbers.
* GetRxItems(rxNumber) - Allows Paladin POS to query the pharmacy system for a specific Rx number, returning any number of results back to the system.
* SaveRxInvoice(rxInvoice) – Allows Paladin POS to inform the pharmacy system of the sale of specific RxItems.
* GetAvailableCredit(customerId) – Allows Paladin POS to query the pharmacy system for a specific customer id, returning the amount of available credit the customer has available as well as the total account balance for the account. 
* SaveInvoice(invoice) – Allows Paladin POS to send an entire invoice to the pharmacy system. Invoice contains all from paladin system. Invoice is sent regardless of any pharmacy items are on the invoice. 

## GetRxItem Method
Allows Paladin POS to query the pharmacy system for a specific Rx number. During checkout, Paladin POS will call the pharmacy webservice GetRxItem in order to try to find the part number entered into the system. The pharmacy service implementation should respond back to paladin indicating that the item was invalid (not found) or valid (found). 

### GetRxItem Request

### GetRxItem Response
If an Rx number is invalid, the webservice should return an RxItem containing the following values:
* RxNumber - The original Rx Number that was passed into the webservice call
* RxValid - Set to “false”
* RxMessages - Containing any number of _RxMessage_ entries
  * Message - _Attribute_ - Text for why item was invalid (ie. Not found, etc…)
  * Print - _Attribute_ - Print message “true” or “false”. Attribute value ignored

**_All other elements are ignored/missing_**

####Example - RxItem Invalid
``` xml
<?xml version="1.0" encoding="utf-8"?>
<RxItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://services.Paladin POS.com/PaladinPharmacyCOMv1">
  <RxNumber>Test Invalid Rx Item 1</RxNumber>
  <RxValid>false</RxValid>
  <RxAmountDue>0</RxAmountDue>
  <RxMessages>
    <RxMessage Message="Item does not exist in the system!" Print="false" />
  </RxMessages>
  <RxItemFlags />
</RxItem>
```
If an Rx number is valid, the webservice should return an RxItem containing the following values:
*	RxNumber - The original Rx Number that was passed into the webservice call
*	RxValid - Set to “true”
*	RxCustomerName - Full name of customer on prescription
*	RxCustomerFName - _optional_ - Customer first name
*	RxCustomerMName - _optional_ - Customer middle name
*	RxCustomerLName - _optional_ - Customer last name
*	RxPatientID - _optional_ - Customer ID from pharmacy system
*	RxCustomerPhone1 - _optional_ - Customer phone number
* RxCustomerPhone2 - _optional_ - Customer phone number (added v1.07)
* RxCustomerEmail1 - _optional_ - Customer email address (added v1.07)
* RxCustomerEmail2 - _optional_ - Customer email address (added v1.07)
*	RxCustomerRegAddress1 - _optional_ - Customer address line 1
*	RxCustomerRegAddress2 - _optional_ - Customer address line 2
*	RxCustomerRegCity - _optional_ - Customer city
*	RxCustomerRegState - _optional_ - Customer state
*	RxCustomerRegZIP - _optional_ - Customer zip
* RxCustomerDeliveryAddress1 - _optional_ - Customer delivery address line 1 (added v1.07)
* RxCustomerDeliveryAddress2 - _optional_ - Customer delivery address line 2 (added v1.07)
* RxCustomerDeliveryCity - _optional_ - Customer delivery city (added v1.07)
* RxCustomerDeliveryState - _optional_ - Customer delivery state (added v1.07)
* RxCustomerDeliveryZIP - _optional_ - Customer delivery zip (added v1.07)
*	RxAmountDue - Total price to charge customer for item.
*	RxTaxable - Charge tax for this item. “true” or “false”.
*	RxMessages - Containing any number of RxMessage entries.
 *	Message - _Attribute_ - Text for message. Up to 62 characters per message.
 *	Print - _Attribute_ - Print message on receipt (“true” or “false”).
*	RxItemFlags - Containing any number of valid RxItemFlag entries.
 *	Type - _Attribute_ - Flag type numerical value. 
	*	Type 1 – HIPPA agreement.
	*	Type 2 – Safety cap agreement.
	*	Type 3 – Pharmacy consult offered
 *	Message - _Attribute_ - Message (if applicable) associated with flag.
 *	Required - _Attribute_ - Client flag condition is required to be accepted.

#### Example - RxItem Valid 
``` xml
<?xml version="1.0" encoding="utf-8"?>
<RxItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://services.Paladin POS.com/PaladinPharmacyCOMv1">
	<RxNumber>123456789</RxNumber>
	<RxValid>true</RxValid>
	<RxCustomerName>Test Customer</RxCustomerName>
	<RxPatientID>111222333444555666</RxPatientID>
	<RxAmountDue>50.00</RxAmountDue>
	<RxTaxable>true</RxTaxable>
	<RxMessages>
	  <RxMessage Message="This item is a screen only message" Print="false" />
	  <RxMessage Message="This message is for everyone!" Print="true" />
	</RxMessages>
	<RxItemFlags>
	  <RxItemFlag Type="1" Message="HIPPA First Time Signature" Required="false" />
	  <RxItemFlag Type="2" Message="No Safety Cap on Rx" Required="true" />
	  <RxItemFlag Type="3" Message="Need Pharmacist Consult" Required="true" />
	</RxItemFlags>
</RxItem>
```

## GetRxItems Method
This method functions the same as GetRxItem, but returns a list or array of RxItems back to Paladin POS from a single item number. This feature is enables support of “Bag Checkout”, where a single barcode represent multiple items, get scanned and all the contents of the “bag” will display on the POS invoice. 
This method was added in v1.06 and is only called when option “Enable GetRxItems (Bag Checkout)” is enabled within Paladin. When enabled, GeRxItems will always be called instead of the original GetRxItem method.

### GetRxItems Request

### GetRxItems Response
``` xml
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfRxItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://services.paladinpos.com/PaladinPharmacyCOMv1">
  <RxItem>
    <RxNumber>test-1</RxNumber>
    <RxValid>true</RxValid>
    <RxCustomerName>Test Customer</RxCustomerName>
    <RxPatientID>111222333444555666</RxPatientID>
    <RxCustomerRegAddress1>321 Bogus Ave.</RxCustomerRegAddress1>
    <RxCustomerRegCity>HomeTown</RxCustomerRegCity>
    <RxCustomerRegState>OK</RxCustomerRegState>
    <RxCustomerRegZIP>34567</RxCustomerRegZIP>
    <RxAmountDue>44.44</RxAmountDue>
    <RxTaxable>true</RxTaxable>
    <RxMessages>
      <RxMessage Message="This item is a screen only message" Print="false"/>
      <RxMessage Message="This message is for everyone!" Print="true"/>
    </RxMessages>
    <RxItemFlags>
      <RxItemFlag Type="1" Message="HIPPA First Time Signature" Required="false"/>
      <RxItemFlag Type="2" Message="No Safety Cap on Rx" Required="true"/>
      <RxItemFlag Type="3" Message="Need Pharmacist Consult" Required="true"/>
    </RxItemFlags>
  </RxItem>
  <RxItem>
    <RxNumber>test-1-2</RxNumber>
    <RxValid>true</RxValid>
    <RxCustomerName>Test Customer</RxCustomerName>
    <RxPatientID>111222333444555666</RxPatientID>
    <RxCustomerRegAddress1>321 Bogus Ave.</RxCustomerRegAddress1>
    <RxCustomerRegCity>HomeTown</RxCustomerRegCity>
    <RxCustomerRegState>OK</RxCustomerRegState>
    <RxCustomerRegZIP>34567</RxCustomerRegZIP>
    <RxAmountDue>44.44</RxAmountDue>
    <RxTaxable>true</RxTaxable>
    <RxMessages>
      <RxMessage Message="This item is a screen only message" Print="false"/>
      <RxMessage Message="This message is for everyone!" Print="true"/>
    </RxMessages>
    <RxItemFlags>
      <RxItemFlag Type="1" Message="HIPPA First Time Signature" Required="false"/>
      <RxItemFlag Type="2" Message="No Safety Cap on Rx" Required="true"/>
      <RxItemFlag Type="3" Message="Need Pharmacist Consult" Required="true"/>
    </RxItemFlags>
  </RxItem>
</ArrayOfRxItem>
```


## SaveRxInvoice Method
When an invoice is completed in Paladin POS that contains RxItems, Paladin POS will return an RxInvoice containing basic invoice details as well as all of the RxItems that were sold as part of the transaction. 
A RxInvoice contains the following values:

* RxInvoiceNumber - Invoice number from Paladin POS
* RxInvoiceDate - Date and time of the invoice transaction
* RxSubTotal - Subtotal of all items on the invoice
 * _Note: Value can include non-pharmacy items_
* RxTaxTotal - Amount of tax changed on the invoice items
 * _Note: Value can include non-pharmacy items_
* RxTotal - Total amount changed on the invoice. 
 * _Note: Value is same as RxSubTotal + RxTaxTotal
* RxItems - Containing any number of RxItem entries
 * RxNumber - Rx Number on invoice
 * RxValid - Set to “true”
 * RxCustomerName - Name of customer
 * RxAmountDue - Amount collected for item
 * **_All other elements should be ignored or empty_**
* RxPayments - Containing any number of RxPayment entries
 * Type - _Attribute_ - Type of invoice payment. Values:
	* 1 - Cash
	* 2 - Charge
	* 3 - Credit Card
	* 4 - Check
	* 5 - Coupon
	* 6 - [Reserved]
	* 7 - [Reserved]
	* 8 - Change
	* 9 - ROA Cash
	* 10 - Discount
	* 11 - ROA Credit Card
	* 12 - ROA Check
	* 13 - ROA Change
	* 14 - Cash Paid-Out
	* 15 - [Reserved]
	* 16 - Cash Drop
	* 17 - Instant Savings Coupons (ACE)
	* 18 - Dynamic Promotions Coupons (ACE)
 * Amount - _Attribute_ - Amount paid for payment type
 * Message - _Attribute_ - Additional payment type information/notes
* RxItemFlagResults - Containing any number of RxItemFlagResult entries
 * CustomerName - _Attribute_ - Name of customer flag applied to
 * Type - _Attribute_ - Flag type
 * Accepted - _Attribute_ - Accepted by customer, “true” or “false”
* Signature - Base64 encoding of JPEG signature. If not accepted, element will be empty/missing

####Example – RxInvoice
``` xml
<?xml version="1.0" encoding="utf-8"?>
<RxInvoice xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://services.Paladin POS.com/PaladinPharmacyCOMv1">
  <RxInvoiceNumber>9201923</RxInvoiceNumber>
  <RxInvoiceDate>2008-03-03T10:17:33.578125-08:00</RxInvoiceDate>
  <RxSubTotal>125.99</RxSubTotal>
  <RxTaxTotal>7.24</RxTaxTotal>
  <RxTotal>133.23</RxTotal>
  <RxItems>
    <RxItem>
      <RxNumber>TEST RX NUMBER</RxNumber>
      <RxValid>true</RxValid>
      <RxCustomerName>Test Customer 1</RxCustomerName>
      <RxAmountDue>50.00</RxAmountDue>
      <RxTaxable>false</RxTaxable>
      <RxMessages />
      <RxItemFlags />
    </RxItem>
    <RxItem>
      <RxNumber>OTHER RX NUMBER</RxNumber>
      <RxValid>true</RxValid>
      <RxCustomerName>Test Customer 1</RxCustomerName>
      <RxAmountDue>50.00</RxAmountDue>
      <RxTaxable>false</RxTaxable>
      <RxMessages />
      <RxItemFlags />
    </RxItem>
  </RxItems>
  <RxPayments>
    <RxPayment Type="1" Amount="20.25" Message="Cash" />
  </RxPayments>
  <RxItemFlagResults>
    <RxItemFlagResult CustomerName="Test Customer 1" Type="1" Accepted="true">
      <Signature>/9j/4AAQSkZJRgigD/9k=</Signature>
    </RxItemFlagResult>
    <RxItemFlagResult CustomerName="Test Customer 1" Type="2" Accepted="true">
      <Signature>/9j/4AAQSkZJRgABQB//Z</Signature>
    </RxItemFlagResult>
    <RxItemFlagResult CustomerName="Test Customer 1" Type="3" Accepted="true">
      <Signature>/9j/KKAKKKACiiigD/9k=</Signature>
    </RxItemFlagResult>
  </RxItemFlagResults>
</RxInvoice>
```
**_Signature values shortened for example_**

## GetAvailableCredit Method
**_Note: Added v1.03. 
Method is only called when option “Enable Customer Available Credit” is enabled within Paladin POS. This option disables AR in Paladin POS to be managed solely by the pharmacy system._**

When an invoice customer is changed within the system, a request for that customer’s available credit will be made. The response from the pharmacy system will determine if a customer can charge to their store credit account maintained by the pharmacy system.

### GetAvailableCredit Request
When enabled, Paladin POS will call GetAvailableCredit on pharmacy system using the customer’s pharmacy system id.

#### Example - GetAvailableCredit Request
``` xml
<GetAvailableCredit xmlns="http://services.paladinpos.com/PaladinPharmacyCOMv1"> 
  <customerId>111222333444555666</customerId> 
</GetAvailableCredit>
```

### GetAvailableCredit Response

The pharmacy system should return a AvailableCreditResponse message back to Paladin POS. The response from the pharmacy should specify if the customer lookup was successful and/or if the customer has any credit to allow charging locally.
As of version 1.05, the available credit response also returns the account balance to allow paladin to display current account information and accept payment on customer accounts on behalf of the pharmacy system.

The AvailableCreditResponse message contains: 
* CustomerId - The customer id found in the pharmacy system, should match value sent in the original request
* IsValid - “true” or “false”, valid customer found by the pharmacy system
* Message - Any messages/errors returned by the pharmacy system
* AvailableCredit - Customer available credit in pharmacy system
 * _Note: AvailableCredit must be greater than zero to allow Paladin POS to charge to their accounts_
 * _Note: Paladin POS will only allow customers to charge up to the maximum value specified by AvailableCredit_
* AccountBalance - **_Added v.1.05_** -  Customer total account balance owed 
 * _Note: If account has a credit balance, AccountBalance should be return a negative value_

#### Example – AvailableCreditResponse Invalid
``` xml
<AvailableCreditResponse> 
  <CustomerId>111222333444555666</CustomerId> 
  <IsValid>false</IsValid> 
  <Message>Customer not found</Message> 
  <AvailableCredit>0</AvailableCredit> 
  <AccountBalance>0</AccountBalance> 
</AvailableCreditResponse> 
```
#### Example – AvailableCreditResponse Valid
``` xml
<AvailableCreditResponse> 
  <CustomerId>111222333444555666</CustomerId> 
  <IsValid>true</IsValid> 
  <Message>success</Message> 
  <AvailableCredit>213.35</AvailableCredit> 
  <AccountBalance>100.00</AccountBalance> 
</AvailableCreditResponse> 
```

## SaveInvoice Method
**_Note: Added v1.03. 
This method is only called when option “Enable Send Full Invoice” is enabled within Paladin. This options disables PaladinPOS calls to SaveRxInvoice, and sends all invoices processed be the pharmacy system.

When an invoice is processed by Paladin POS, the entire invoice will be sent to the Pharmacy System. This should only be used if the pharmacy system is used as the primary system for AR.

An Invoice contains the following fields:
* Id - int - Paladin Invoice Id
* Deleted - bool/empty - Invoice deleted status
* Number - int/empty - Alternate invoice number
* Date - date/empty - Invoice date
* AccountNumber - int/empty - Paladin customer id
* AccountName - string - Customer account name
* PharmacyAccountNumber - string - Pharmacy customer id 
* AccountTracker - int/empty
* TerminalNumber - int/empty - Paladin terminal number
* EmployeeNumber - int/empty - Paladin Employee id
* PurchaseOrderNumber - string/empty - Purchase order number
* Total - decimal/empty - Invoice total amount
* Profit - decimal/empty - Invoice profit amount
* PromptPaymentDiscount - decimal/empty
* GlobalTax - bool/empty - Invoice global tax flag status
* GlobalNet - bool/empty - Invoice global net flag status
* GlobalDefective - bool/empty - Invoice global defective flag status
* DefaultTaxOverride - bool/empty - Invoice default tax override flag status
* InvoiceType - int/empty - Invoice Type
* Signature – byte[]/empty - Default invoice signature. Base64 encoding of JPEG signature. If no signature, element will be empty/missing
* Expansion - string/empty
* FlexTotal - decimal/empty - Invoice flex eligible item total amount
* StoreId - int/empty - Invoice store id
* CustomerRewardNumber - string/empty - Customer rewards number
* Categories - int/empty
* InvoiceItems - array - List all InvoiceItem items on invoice
 * InvoiceId – int – 
 * LineNumber – int – Invoice line number
 * InventoryId – int/empty – 
 * Date – datetime/empty – 
 * PartNumber – string/empty – 
 * Description – string/empty –
 * PrimaryNumber – string/empty –
 * DepartmentId – int/empty –
 * QuantitySold – decimal/empty – 
 * ReorderQuantity – decimal/empty –
 * RetailPrice – decimal/empty –
 * SellPrice – decimal/empty –
 * SoldPrice – decimal/empty –
 * SellThreshold – int/empty –
 * Taxable – bool/empty –
 * Net – bool/empty –
 * Defective – bool/empty –
 * LineExt – decimal/empty –
 * PromptPaymentDiscount – decimal/empty –
 * Profit – decimal/empty –
 * CostPerUnit – decimal/empty –
 * CostLineExt – decimal/empty –
 * PriceType – int/empty –
 * PricingPlanId – int/empty –
 * SaleId – int/empty –
 * SupplierId1 – int/empty –
 * SupplierId2 – int/empty –
 * ClassId1 – int/empty –
 * SubclassId1 – int/empty –
 * ClassId2 – int/empty –
 * SubclassId2 – int/empty –
 * ClassId3 – int/empty –
 * SubclassId3 – int/empty –
 * Expansion – string/empty –
 * FlexItem – bool/empty –
 * StoreId – int/empty –
 * Categories – int/empty –
 * RxItem **_Added v.1.04_** - bool/empty – Invoice item is an RxItem/Pharmacy item
* InvoiceText - array - List of all InvoiceText items on invoice. InvoiceText messages contain:
 * InvoiceId - int - Invoice id
 * LineNumber - int - Invoice line number
 * Value - string/empty - text/note value
 * Categories - int/empty 
 * Expansion - string/empty
* InvoicePayments - array - List of all <InvoicePayment> items on invoice. InvoicePayment messages contain:
 * InvoiceId - int - Invoice id
 * LineNumber - int - Invoice line number
 * Date - date/empty - Payment date
 * Type - int/empty - Type of payment made
	* 1 - Cash
	* 2 - Charge
	* 3 - Credit Card
	* 4 - Check
	* 5 - Coupon
	* 6 - [Reserved]
	* 7 - [Reserved]
	* 8 - Change
	* 9 - ROA Cash
	* 10 - Discount
	* 11 - ROA Credit Card
	* 12 - ROA Check
	* 13 - ROA Change
	* 14 - Cash Paid-Out
	* 15 - [Reserved]
	* 16 - Cash Drop
	* 17 - Instant Savings Coupons (ACE)
	* 18 - Dynamic Promotions Coupons (ACE)
 * Amount - decimal/empty - Total amount of tax due
 * Data - string/empty - XML data, containing any extra payment specific details
 * SaleId - int/empty - SaleId associated with payment
 * Categories - int/empty 
 * Expansion - string/empty
* InvoiceTaxes - array - List of all <InvoiceTax> items on invoice. InvoiceTax messages contain:
 * Id - int - Payment Id
 * InvoiceId - int - Invoice id
 * LineNumber - int - Invoice line number
 * Name - string/empty - Name of tax
 * Rate - decimal/empty - Tax rate
 * Amount - decimal/empty - Total amount of tax due
 * Categories - int/empty 
 * Expansion - string/empty
* InvoiceRxItemFlags **_Added v.1.04_** - Containing any number of InvoiceRxItemFlag entries. InvoiceRxItemFlag messages contain:
 * CustomerId – string/empty – Customer id associated with flag
 * CustomerName – string/empty – Customer name of customer flag applied to
 * Type – int/empty – Rx Item flag type
 * Accepted – bool/empty - Accepted by customer, “true” or “false”
 * Signature - byte[]/empty - Base64 encoding of signature. If not accepted, element will be empty/missing

#### Example – SaveInvoice Request
``` xml
<?xml version="1.0" encoding="utf-8"?>
<SaveInvoice xmlns="http://services.paladinpos.com/PaladinPharmacyCOMv1">
  <invoice>
	<Id>28841</Id>
	<Deleted>false</Deleted>
	<Number>28841</Number>
	<Date>2013-11-22T13:16:51.384</Date>
	<AccountNumber>2332</AccountNumber>
	<AccountName>Test Customer</AccountName>
	<PharmacyAccountNumber>111222333444555666</PharmacyAccountNumber>
	<AccountTracker>0</AccountTracker>
	<TerminalNumber>4</TerminalNumber>
	<EmployeeNumber>1</EmployeeNumber>
	<PurchaseOrderNumber />
	<Total>47.77</Total>
	<Profit>44.44</Profit>
	<PromptPaymentDiscount>0</PromptPaymentDiscount>
	<GlobalTax>true</GlobalTax>
	<GlobalNet>false</GlobalNet>
	<GlobalDefective>false</GlobalDefective>
	<DefaultTaxOverride>false</DefaultTaxOverride>
	<InvoiceType>0</InvoiceType>
	<Signature>R0lGODlhkAGAAPcAAAAAAAAAMwAAZgAAmQAAzAAA/TYMkfFMWEQVByJXc7UqEpvdIzemlvlbCMpSxhExAAOw==</Signature>
	<FlexTotal xsi:nil="true" />
	<StoreId xsi:nil="true" />
	<CustomerRewardNumber>111222333444555666</CustomerRewardNumber>
	<Categories xsi:nil="true" />
	<InvoiceItems>
	  <InvoiceItem>
		<InvoiceId>28841</InvoiceId>
		<LineNumber>1</LineNumber>
		<InventoryId>-2147483648</InventoryId>
		<Date>2013-11-22T13:16:51.384</Date>
		<PartNumber>123456</PartNumber>
		<Description>Pharmacy Item 123456</Description>
		<PrimaryNumber />
		<DepartmentId>10020</DepartmentId>
		<QuantitySold>1</QuantitySold>
		<ReorderQuantity>0</ReorderQuantity>
		<RetailPrice>44.44</RetailPrice>
		<SellPrice>44.44</SellPrice>
		<SoldPrice>44.44</SoldPrice>
		<SellThreshold>0</SellThreshold>
		<Taxable>true</Taxable>
		<Net>false</Net>
		<Defective>false</Defective>
		<LineExt>44.44</LineExt>
		<PromptPaymentDiscount>0</PromptPaymentDiscount>
		<Profit>44.44</Profit>
		<CostPerUnit>0</CostPerUnit>
		<CostLineExt>0</CostLineExt>
		<PriceType xsi:nil="true" />
		<PricingPlanId>0</PricingPlanId>
		<SaleId>0</SaleId>
		<SupplierId1>-1</SupplierId1>
		<SupplierId2>-1</SupplierId2>
		<ClassId1>-1</ClassId1>
		<SubclassId1>-1</SubclassId1>
		<ClassId2>-1</ClassId2>
		<SubclassId2>-1</SubclassId2>
		<ClassId3>-1</ClassId3>
		<SubclassId3>-1</SubclassId3>
		<FlexItem>true</FlexItem>
		<StoreId xsi:nil="true" />
		<Categories xsi:nil="true" />
		<RxItem>true</RxItem>
	  </InvoiceItem>
	</InvoiceItems>
	<InvoiceText>
	  <InvoiceText>
		<InvoiceId>28841</InvoiceId>
		<LineNumber>2</LineNumber>
		<Value>This message is for everyone!</Value>
		<Categories xsi:nil="true" />
	  </InvoiceText>
	</InvoiceText>
	<InvoicePayments>
	  <InvoicePayment>
		<InvoiceId>28841</InvoiceId>
		<LineNumber>3</LineNumber>
		<Date>2013-11-22T13:16:51.384</Date>
		<Type>2</Type>
		<Amount>47.77</Amount>
		<Data>&lt;Charge&gt;
&lt;AccountNumber&gt;2332&lt;/AccountNumber&gt;
&lt;Name /&gt;
&lt;Memo /&gt;
&lt;/Charge&gt;</Data>
		<SaleId xsi:nil="true" />
		<Categories xsi:nil="true" />
	  </InvoicePayment>
	</InvoicePayments>
	<InvoiceTaxes>
	  <InvoiceTax>
		<Id>83878</Id>
		<InvoiceId>28841</InvoiceId>
		<Name>Florida Sales Tax</Name>
		<Type>1</Type>
		<Rate>7.5</Rate>
		<Amount>3.33</Amount>
		<Categories xsi:nil="true" />
	  </InvoiceTax>
	  <InvoiceTax>
		<Id>83879</Id>
		<InvoiceId>28841</InvoiceId>
		<Name>None</Name>
		<Type>2</Type>
		<Rate>0</Rate>
		<Amount>0</Amount>
		<Categories xsi:nil="true" />
	  </InvoiceTax>
	  <InvoiceTax>
		<Id>83880</Id>
		<InvoiceId>28841</InvoiceId>
		<Name>None</Name>
		<Type>3</Type>
		<Rate>0</Rate>
		<Amount>0</Amount>
		<Categories xsi:nil="true" />
	  </InvoiceTax>
	</InvoiceTaxes>
	<InvoiceRxItemFlags>
	  <InvoiceRxItemFlag>
		<CustomerId>111222333444555666</CustomerId>
		<CustomerName>Test Customer</CustomerName>
		<Type>1</Type>
		<Accepted>true</Accepted>
		<Signature>R0lGODlhkAGAAPcAAAAAAAAAMwAAZgAAmQAAzAAA/lbCMpSxhExAAOw==</Signature>
	  </InvoiceRxItemFlag>
	</InvoiceRxItemFlags>
  </invoice>
</SaveInvoice>
```
# PharmacyCOM Encryption
The project PaladinPharmacyCOMv1.Encryption can be used to encrypt message traffic. The encryption is done via a SOAP extension method which can be
enabled via your project Web.Config (see Sample project). An encryption password is defined in the SOAP extension. This can be changed if desired but
must be coordinated with Paladin. Also must be coordinated with Paladin whether encryption is enabled or disabled.

**_Signature values shortened for example_**
