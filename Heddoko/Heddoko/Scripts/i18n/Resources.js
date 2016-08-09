var i18n = i18n||{};
i18n.Resources = (function () { 
	var strings = {
  "AnatomicalLocationType_LeftForeArm": "Left Fore Arm",
  "AnatomicalLocationType_LeftThigh": "Left Thigh",
  "AnatomicalLocationType_LeftTibia": "Left Tibia",
  "AnatomicalLocationType_LeftUpperArm": "Left Upper Arm",
  "AnatomicalLocationType_RightForeArm": "Right Fore Arm",
  "AnatomicalLocationType_RightThigh": "Right Thigh",
  "AnatomicalLocationType_RightTibia": "Right Tibia",
  "AnatomicalLocationType_RightUpperArm": "Right Upper Arm",
  "AnatomicalLocationType_UpperSpine": "Upper Spine",
  "ConditionType_New": "New",
  "ConditionType_Used": "Used",
  "Confirmed": "Confirmed",
  "ConfirmPassword": "Confirm password",
  "ConfirmPasswordValidateMessage": "The passwords do not match",
  "ConfirmToken": "Confirm token",
  "CreateNewAccount": "Create a new account",
  "Email": "Email",
  "EmailActivatedBody": "You have been activated",
  "EmailActivatedSubject": "Activated!",
  "EmailActivationUserSubject": "Welcome!",
  "EmailForgotPasswordSubject": "Forgot password",
  "EmailForgotUsernameSubject": "Remind username",
  "EmptyConfirmationToken": "Empty confirmation token",
  "EmptyForgotToken": "Empty forgot token",
  "EquipmentStatusType_Available": "Available",
  "EquipmentStatusType_InTransit": "In transit",
  "EquipmentStatusType_OnLoan": "On loan",
  "EquipmentStatusType_Unavailable": "Unavailable",
  "ExpiredForgotToken": "Forgot token is expired",
  "ForgotYourPassword": "Forgot your password?",
  "ForgotYourUsername": "Forgot your username?",
  "HeatsShrinkType_No": "No",
  "HeatsShrinkType_Yes": "Yes",
  "Heddoko": "Heddoko",
  "Login": "Login",
  "MaterialType_Battery": "Battery",
  "MaterialType_Sensor": "Sensor",
  "MovementEventType_Jump": "Jump",
  "MovementEventType_Standing": "Standing",
  "NewPassword": "New password",
  "NumbersType_No": "No",
  "NumbersType_Yes": "Yes",
  "Password": "Password",
  "PasswordSuccessufullyChanged": "Password Successfully Changed",
  "PrototypeType_No": "No",
  "PrototypeType_Yes": "Yes",
  "ShipType_Gone": "Gone",
  "ShipType_No": "No",
  "ShipType_Yes": "Yes",
  "SignInInspiring": "More Than Just a Coach",
  "Title": "Heddoko | Track your movement in 3D",
  "UserGenderType_Female": "Female",
  "UserGenderType_Male": "Male",
  "UserGenderType_NotSpecified": "NotSpecified",
  "UserIsBanned": "User is banned",
  "Username": "Username",
  "UserRoleType_Admin": "Admin",
  "UserRoleType_Analyst": "Analyst",
  "UserRoleType_User": "User",
  "UserStatusType_Active": "Active",
  "UserStatusType_Banned": "Banned",
  "UserStatusType_NotActive": "Inactive",
  "ValidateEmailMessage": "The E-mail field is not a valid e-mail address",
  "ValidateLengthMessage": "The {0} must be at least {2} characters long.",
  "ValidateLengthRangeMessage": "The {0} must be from {2} to {1} characters long",
  "ValidateMaxLengthMessage": "The {0} cannot be longer than {2} characters long.",
  "ValidateRequiredMessage": "The {0} field is required",
  "WrongConfirmationToken": "Wrong confirmation token",
  "WrongEmailForgotPassword": "No account found with that email address",
  "WrongForgotToken": "Invalid forgot token",
  "WrongUsernameOrPassword": "These credentials do not match our records.",
  "YouAreNotAllowed": "You are not allowed",
  "YouAreNotAuthorized": "You are not authorized",
  "Oops": "<strong>Whoops!</strong> There were some problems with your input.<br><br>",
  "PasswordForgotMessage": "Enter the email address that you used to register. We'll send you an email with your username and a link to reset your password.",
  "ResetPassword": "Reset password",
  "RetrieveUsername": "Retrieve Username",
  "UsernameForgotMessage": "Enter the email address that you used to register. We'll send you an email with your username.",
  "AccountType": "Account Type",
  "Birthday": "Date of Birth",
  "Confirm": "Confirm",
  "ConfirmMessage": "Use this form to confirm your account",
  "Country": "Country",
  "EmailAddress": "Email Address",
  "Firstname": "First Name",
  "Lastname": "Last Name",
  "Mobile": "Mobile",
  "PasswordSuccessufullySent": "We have sent an email with a link to reset a password",
  "ResetPasswordMessage": "Use this form to reset your password.",
  "UsernameSuccessufullySent": "We have sent an email with your username.",
  "EmailUsed": "Email already used",
  "UsernameUsed": "Username already used",
  "UserSignupMessage": "User have been created. We have sent email with activation link, please activate your account.",
  "And": "and",
  "Privacy": "privacy policy",
  "SignInInviteMessage": "Already have an account?",
  "SignInMessage": "Log in now",
  "SignUp": "Sign up",
  "TermMessage": "By clicking on Sign up, you agree to <br> our",
  "Terms": "terms & conditions",
  "UserIsNotActive": "User is not activated",
  "Actions": "Actions",
  "AdminTitle": "Heddoko Admin",
  "Dashboard": "Dashboard",
  "Delete": "Delete",
  "Edit": "Edit",
  "EmptyItems": "No item to display",
  "ID": "ID",
  "Identifier": "Identifier",
  "SignOut": "Sign out",
  "WrongObjectAccess": "You don't access to that object",
  "AddANew": "Add a new",
  "AnatomicalLocation": "Anatomical location",
  "AnatomicalLocations": "Anatomical locations",
  "Equipment": "Equipment",
  "Equipments": "Equipments",
  "Error": "Error",
  "Find": "Find",
  "Home": "Home",
  "Information": "Information",
  "LicenseTitle": "License dashboard",
  "Material": "Material",
  "Materials": "Materials",
  "MaterialType": "Material type",
  "Reset": "Reset",
  "Search": "Search",
  "Submit": "Submit",
  "Suit": "Suit",
  "Suits": "Suits",
  "Warning": "Warning",
  "MaterialTypes": "Material Types",
  "CannotAddDuplicate": "Can't add duplicate",
  "EnterIdentifier": "Enter an identifier",
  "EnterMaterial": "Enter the name or part # of a material",
  "Name": "Name",
  "PartNo": "Part #",
  "CannotRemove": "Can't delete, because",
  "Use": "use",
  "EnterEquipment": "Enter the serial # or location or mac address of an equipment",
  "Notes": "Notes",
  "Condition": "Condition",
  "HeatsShrink": "Heatshrink good",
  "MacAddress": "MAC Address",
  "Numbers": "Numbers",
  "PhysicalLocation": "Physical location",
  "Prototype": "Prototype",
  "SelectAnatomicalLocation": "Select an anatomical location",
  "SelectCondition": "Select a condition",
  "SelectHeatsShrink": "Select a heats shrink",
  "SelectMaterial": "Select a material",
  "SelectMaterialType": "Select a material type",
  "SelectNumber": "Select a number",
  "SelectPrototype": "Select a prototype",
  "SelectShip": "Select a ready ship",
  "SelectStatus": "Select a status",
  "SelectVerifiedBy": "Select verified by",
  "SerialNo": "Serial #",
  "Ship": "Ship",
  "Status": "Status",
  "UserRoleType_LicenseAdmin": "License manager",
  "VerifiedBy": "Verified by",
  "Add": "Add",
  "ComplexEquipment": "Suit",
  "ComplexEquipments": "Suits",
  "EnterComplexEquipment": "Enter a MAC address, a physical location, or a serial #",
  "Admin": "Admin",
  "Required": "required",
  "Link": "Link",
  "None": "None",
  "Unlink": "Unlink",
  "Save": "Save",
  "SelectEquipment": "Select a equipments",
  "Address": "Address",
  "Organization": "Organization",
  "Organizations": "Organizations",
  "Phone": "Phone",
  "EnterOrganization": "Enter the name or id or phone of organization",
  "UserIsExists": "User already exists",
  "EmailInviteAdminUserSubject": "You become as admin of organization",
  "UserAlreadyInOrganizations": "User used in another Organization",
  "InviteMessage": "Use this form to apply you invite",
  "InviteToken": "Invite token",
  "UserSignupOrganizationMessage": "You have been updated. Now you can use your organization",
  "Amount": "Amount",
  "ExpirationAt": "Expiration at",
  "License": "License",
  "LicenseUsed": "License already used",
  "LiceseAmountUsed": "License amount can't be less than used",
  "Type": "Type",
  "UserSignupUserNonLicenseOrganizationMessage": "You have been updated. Ask admin of your organization for license.",
  "UserSignupUserOrganizationMessage": "You have been updated. Now you can use your account in application.",
  "LicenseType_DataAnalysis": "Data analysis",
  "LicenseType_DataCollection": "Data collection",
  "DataAnalysisAmount": "Data anaylysis amount",
  "DataCollectorAmount": "Data collectors amount",
  "OrganizationName": "Name of organization",
  "Used": "Used",
  "OrganizationNameUsed": "The name of organization in use.",
  "Licenses": "Licenses",
  "User": "User",
  "Users": "Users",
  "EnterLicense": "Enter the id of license",
  "EnterUser": "Enter the firstname or lastname or email of user",
  "Role": "Role",
  "EmailInviteUserSubject": "You invited to organization",
  "EmptyLicense": "No license",
  "Profile": "Profile",
  "OldPassword": "Old password",
  "ProfileSaveMessage": "You profile have been updated",
  "WrongLicenseActive": "License is not active",
  "WrongLicenseDeleted": "License is removed",
  "WrongLicenseExpiration": "License is expired",
  "WrongExpirationAtDate": "Expiration date should be more than today",
  "Restore": "Restore",
  "ShowDeleted": "Show deleted",
  "WrongLicenseAdmin": "Admin can't be a license admin",
  "Attachments": "Attachments",
  "DetailedDescription": "Detailed Description",
  "FullName": "Full name",
  "Importance": "Issue Importance",
  "IssueImportance_High": "Show stopper",
  "IssueImportance_Low": "Can live with it now",
  "IssueImportance_Medium": "Doesn't prevent me from working",
  "IssueType": "Issue type",
  "IssueType_Hardware": "Hardware Issue",
  "IssueType_NewFeature": "New Feature Request",
  "IssueType_Software": "Software Issue",
  "Send": "Send",
  "ShortDescription": "Short Description",
  "Support": "Support",
  "SupportSent": "We have sent an email with your support request. Soon we will contact with you.",
  "WrongAttachmentSize": "Total size can't be more than 20 mb.",
  "All": "All",
  "ExpiredSoon": "License will be expired soon",
  "LicenseStatusType_Active": "Active",
  "LicenseStatusType_Deleted": "Deleted",
  "LicenseStatusType_Expired": "Expired",
  "LicenseStatusType_Inactive": "Inactive",
  "ShowByLicenses": "Show users by license",
  "Size": "Size",
  "QAStatus": "QA Status",
  "PantsOctopi": "Pants Octopi",
  "ShirtsOctopi": "Shirts Octopi",
  "Inventory": "Inventory",
  "GoodAfternoon": "Good Afternoon",
  "GoodEvening": "Good Evening",
  "GoodMorning": "Good Morning",
  "WhatWouldYouLikeToDO": "What would you like to do?",
  "Manage": "Manage",
  "EnterPantsOctopi": "Enter pants octopi id or size",
  "EnterShirtsOctopi": "Enter shirt octopi id or size",
  "SelectQAStatus": "Select QA status",
  "SelectSize": "Select size",
  "Enter": "Enter",
  "Pants": "Pants",
  "Shirts": "Shirts",
  "Select": "Select",
  "Kits": "Kits",
  "Kit": "Kit",
  "AlreadyUsed": "already used",
  "No": "No",
  "SoftwareAndFirmware": "Software & Firmware",
  "SoftwareOrFirmware": "Software or Firmware",
  "WrongSize": "is the wrong size",
  "Components": "Components",
  "Quantity": "Quantity",
  "Version": "Version",
  "PleaseChooseFileForUpload": "Please choose file for upload",
  "Download": "Download",
  "Url": "Url",
  "FirmwareVersion": "Firmware version",
  "Brainpack": "Brainpack",
  "Brainpacks": "Brainpacks",
  "Databoard": "Databoard",
  "Databoards": "Databoards",
  "Powerboard": "Powerboard",
  "Powerboards": "Powerboards",
  "Composition": "Composition",
  "Shirt": "Shirt",
  "Wrong": "Wrong",
  "NonAssigned": "Non assigned",
  "NotFound": "Not found",
  "Sensor": "Sensor",
  "Sensors": "Sensors",
  "SensorSet": "Sensor set",
  "SensorSets": "Sensor sets",
  "KitID": "Kit ID",
  "EnterSensors": "Enter the serial #, location or position of a sensor",
  "Assemblies": "Assemblies",
  "Assembly": "Assembly",
  "QuantityOnHand": "Quantity on Hand",
  "QuantityProducible": "Quantity Producible",
  "Label": "Label",
  "LabelInfo": "Mapping to old version of serial number",
  "Location": "Location",
  "EquipmentStatusType_Defective": "Defective",
  "EquipmentStatusType_InProduction": "In production",
  "EquipmentStatusType_InUse": "In use",
  "EquipmentStatusType_Ready": "Ready",
  "EquipmentStatusType_Testing": "Testing",
  "EquipmentStatusType_Trash": "Trash",
  "SensorType_EMIMU": "EM IMU",
  "SensorType_NodIMU": "Nod IMU",
  "SensorType_StretchSense": "Stretch Sense",
  "EquipmentQAStatusType_Fail": "Fail",
  "EquipmentQAStatusType_TestedAndReady": "Tested and Ready",
  "DataboardQAStatusType_Fail": "Fail",
  "DataboardQAStatusType_TestedAndReady": "Tested and Ready",
  "KitQAStatusType_Fail": "Fail",
  "KitQAStatusType_TestedAndReady": "Tested and Ready",
  "PowerboardQAStatusType_Fail": "Fail",
  "PowerboardQAStatusType_TestedAndReady": "Tested and Ready",
  "SensorSetQAStatusType_Fail": "Fail",
  "SensorSetQAStatusType_TestedAndReady": "Tested and Ready",
  "ShirtQAStatusType_BaseplateInspectionFail": "Baseplates inspection Fail",
  "ShirtQAStatusType_BaseplateInspectionPass": "Baseplates inspection Pass",
  "ShirtQAStatusType_ConnectorInspectionFail": "Connector inspection Fail",
  "ShirtQAStatusType_ConnectorInspectionPass": "Connector inspection Pass",
  "ShirtQAStatusType_Fail": "Fail",
  "ShirtQAStatusType_HeatShrinkInspectionFail": "Heat Shrink Inspection Fail",
  "ShirtQAStatusType_HeatShrinkInspectionPass": "Heat Shrink Inspection Pass",
  "ShirtQAStatusType_IDLabelInspectionFail": "ID label inspection Fail",
  "ShirtQAStatusType_IDLabelInspectionPass": "ID label inspection Pass",
  "ShirtQAStatusType_PowerInspectionFail": "Power inspection Fail",
  "ShirtQAStatusType_PowerInspectionPass": "Power inspection Pass",
  "ShirtQAStatusType_SeamsInspectionFail": "Seams inspection Fail",
  "ShirtQAStatusType_SeamsInspectionPass": "Seams inspection Pass",
  "ShirtQAStatusType_TestedAndReady": "Tested and Ready",
  "ShirtQAStatusType_WiringInspectionFail": "Wiring inspection Fail",
  "ShirtQAStatusType_WiringInspectionPass": "Wiring inspection Pass",
  "ShirtOctopiQAStatusType_BaseplateInspectionFail": "Baseplates inspection Fail",
  "ShirtOctopiQAStatusType_BaseplateInspectionPass": "Baseplates inspection Pass",
  "ShirtOctopiQAStatusType_ConnectorInspectionFail": "Connector inspection Fail",
  "ShirtOctopiQAStatusType_ConnectorInspectionPass": "Connector inspection Pass",
  "ShirtOctopiQAStatusType_Fail": "Fail",
  "ShirtOctopiQAStatusType_HeatShrinkInspectionFail": "Heat Shrink Inspection Fail",
  "ShirtOctopiQAStatusType_HeatShrinkInspectionPass": "Heat Shrink Inspection Pass",
  "ShirtOctopiQAStatusType_IDLabelInspectionFail": "ID label inspection Fail",
  "ShirtOctopiQAStatusType_IDLabelInspectionPass": "ID label inspection Pass",
  "ShirtOctopiQAStatusType_PowerInspectionFail": "Power inspection Fail",
  "ShirtOctopiQAStatusType_PowerInspectionPass": "Power inspection Pass",
  "ShirtOctopiQAStatusType_SeamsInspectionFail": "Seams inspection Fail",
  "ShirtOctopiQAStatusType_SeamsInspectionPass": "Seams inspection Pass",
  "ShirtOctopiQAStatusType_TestedAndReady": "Tested and Ready",
  "ShirtOctopiQAStatusType_WiringInspectionFail": "Wiring inspection Fail",
  "ShirtOctopiQAStatusType_WiringInspectionPass": "Wiring inspection Pass",
  "PantsQAStatusType_BaseplateInspectionFail": "Baseplates inspection Fail",
  "PantsQAStatusType_BaseplateInspectionPass": "Baseplates inspection Pass",
  "PantsQAStatusType_ConnectorInspectionFail": "Connector inspection Fail",
  "PantsQAStatusType_ConnectorInspectionPass": "Connector inspection Pass",
  "PantsQAStatusType_Fail": "Fail",
  "PantsQAStatusType_HeatShrinkInspectionFail": "Heat Shrink Inspection Fail",
  "PantsQAStatusType_HeatShrinkInspectionPass": "Heat Shrink Inspection Pass",
  "PantsQAStatusType_IDLabelInspectionFail": "ID label inspection Fail",
  "PantsQAStatusType_IDLabelInspectionPass": "ID label inspection Pass",
  "PantsQAStatusType_PowerInspectionFail": "Power inspection Fail",
  "PantsQAStatusType_PowerInspectionPass": "Power inspection Pass",
  "PantsQAStatusType_SeamsInspectionFail": "Seams inspection Fail",
  "PantsQAStatusType_SeamsInspectionPass": "Seams inspection Pass",
  "PantsQAStatusType_TestedAndReady": "Tested and Ready",
  "PantsQAStatusType_WiringInspectionFail": "Wiring inspection Fail",
  "PantsQAStatusType_WiringInspectionPass": "Wiring inspection Pass",
  "PantsOctopiQAStatusType_BaseplateInspectionFail": "Baseplates inspection Fail",
  "PantsOctopiQAStatusType_BaseplateInspectionPass": "Baseplates inspection Pass",
  "PantsOctopiQAStatusType_ConnectorInspectionFail": "Connector inspection Fail",
  "PantsOctopiQAStatusType_ConnectorInspectionPass": "Connector inspection Pass",
  "PantsOctopiQAStatusType_Fail": "Fail",
  "PantsOctopiQAStatusType_HeatShrinkInspectionFail": "Heat Shrink Inspection Fail",
  "PantsOctopiQAStatusType_HeatShrinkInspectionPass": "Heat Shrink Inspection Pass",
  "PantsOctopiQAStatusType_IDLabelInspectionFail": "ID label inspection Fail",
  "PantsOctopiQAStatusType_IDLabelInspectionPass": "ID label inspection Pass",
  "PantsOctopiQAStatusType_PowerInspectionFail": "Power inspection Fail",
  "PantsOctopiQAStatusType_PowerInspectionPass": "Power inspection Pass",
  "PantsOctopiQAStatusType_SeamsInspectionFail": "Seams inspection Fail",
  "PantsOctopiQAStatusType_SeamsInspectionPass": "Seams inspection Pass",
  "PantsOctopiQAStatusType_TestedAndReady": "Tested and Ready",
  "PantsOctopiQAStatusType_WiringInspectionFail": "Wiring inspection Fail",
  "PantsOctopiQAStatusType_WiringInspectionPass": "Wiring inspection Pass",
  "BrainpackQAStatusType_AppConnectsToBrainpackFail": "App connects to to brainpack Fail",
  "BrainpackQAStatusType_AppConnectsToBrainpackPass": "App connects to to brainpack Pass",
  "BrainpackQAStatusType_AppDisconnectsFromBrainpackFail": "App disconnects from brainpack Fail",
  "BrainpackQAStatusType_AppDisconnectsFromBrainpackPass": "App disconnects from brainpack Pass",
  "BrainpackQAStatusType_AppLocatesBrainpackFail": "App locates brainpack Fail",
  "BrainpackQAStatusType_AppLocatesBrainpackPass": "App locates brainpack Pass",
  "BrainpackQAStatusType_AppPairsBrainpackFail": "App pairs brainpack Fail",
  "BrainpackQAStatusType_AppPairsBrainpackPass": "App pairs brainpack Pass",
  "BrainpackQAStatusType_Buttons1mmOfRecessFail": "Buttons 1 mm of recess Fail",
  "BrainpackQAStatusType_Buttons1mmOfRecessPass": "Buttons 1 mm of recess Pass",
  "BrainpackQAStatusType_ButtonsReturnToInitialDepthFail": "Buttons return to initial depth Fail",
  "BrainpackQAStatusType_ButtonsReturnToInitialDepthPass": "Buttons return to initial depth Pass",
  "BrainpackQAStatusType_ChargingIndicatorLedFail": "Charging indicator LED Fail",
  "BrainpackQAStatusType_ChargingIndicatorLedPass": "Charging indicator LED Pass",
  "BrainpackQAStatusType_Fail": "Fail",
  "BrainpackQAStatusType_FullChargingCycleFail": "Full Charging Cycle Fail",
  "BrainpackQAStatusType_FullChargingCyclePass": "Full Charging Cycle Pass",
  "BrainpackQAStatusType_LedColorsFail": "LED colors Fail",
  "BrainpackQAStatusType_LedColorsPass": "LED colors Pass",
  "BrainpackQAStatusType_PairingWithIMUsFail": "Pairing with IMUs Fail",
  "BrainpackQAStatusType_PairingWithIMUsPass": "Pairing with IMUs Pass",
  "BrainpackQAStatusType_PowerButtonWorkingFail": "Power button working Fail",
  "BrainpackQAStatusType_PowerButtonWorkingPass": "Power button working Pass",
  "BrainpackQAStatusType_RecordButtonWorkingFail": "Record button working Fail",
  "BrainpackQAStatusType_RecordButtonWorkingPass": "Record button working Pass",
  "BrainpackQAStatusType_RecordingFilenameSavedFail": "Recording File name saved Fail",
  "BrainpackQAStatusType_RecordingFilenameSavedPass": "Recording File name saved Pass",
  "BrainpackQAStatusType_RecoveryFromShutdownFail": "Recovery from shutdown Fail",
  "BrainpackQAStatusType_RecoveryFromShutdownPass": "Recovery from shutdown Pass",
  "BrainpackQAStatusType_ResetButtonWorkingFail": "Reset button working Fail",
  "BrainpackQAStatusType_ResetButtonWorkingPass": "Reset button working Pass",
  "BrainpackQAStatusType_SDCardIsElectronicallyLabeledWithBatteryPackIdFail": "SD card is electronically labeled with battery pack ID Fail",
  "BrainpackQAStatusType_SDCardIsElectronicallyLabeledWithBatteryPackIdPass": "SD card is electronically labeled with battery pack ID Pass",
  "BrainpackQAStatusType_SetRecordingNameFail": "Set recording name Fail",
  "BrainpackQAStatusType_SetRecordingNamePass": "Set recording name Pass",
  "BrainpackQAStatusType_SettingsFileUpdatedFail": "Settings file updated Fail",
  "BrainpackQAStatusType_SettingsFileUpdatedPass": "Settings file updated Pass",
  "BrainpackQAStatusType_StateChangeIdleToRecordingFail": "State change: idle to recording Fail",
  "BrainpackQAStatusType_StateChangeIdleToRecordingPass": "State change: idle to recording Pass",
  "BrainpackQAStatusType_StateChangeIdleToResetFail": "State change: idle to reset Fail",
  "BrainpackQAStatusType_StateChangeIdleToResetPass": "State change: idle to reset Pass",
  "BrainpackQAStatusType_StateChangeRecordingToErrorFail": "State change: recording to error Fail",
  "BrainpackQAStatusType_StateChangeRecordingToErrorPass": "State change: recording to error Pass",
  "BrainpackQAStatusType_StateChangeRecordingToIdleFail": "State change: recording to idle Fail",
  "BrainpackQAStatusType_StateChangeRecordingToIdlePass": "State change: recording to idle Pass",
  "BrainpackQAStatusType_StateChangeRecordingToResetFail": "State change: recording to reset Fail",
  "BrainpackQAStatusType_StateChangeRecordingToResetPass": "State change: recording to reset Pass",
  "BrainpackQAStatusType_StateIndicatorLedFail": "State indicator LED Fail",
  "BrainpackQAStatusType_StateIndicatorLedPass": "State indicator LED Pass",
  "BrainpackQAStatusType_TestedAndReady": "Tested and Ready",
  "SizeType_ExtraLarge": "Extra Large",
  "SizeType_Large": "Large",
  "SizeType_Medium": "Medium",
  "SizeType_Small": "Small"
};
	return $.extend({}, i18n.Resources || {}, strings);
}());