var i18n = i18n||{};
i18n.Resources = (function () { 
	var strings = {
  "AnatomicalLocationType_Torso": "0 - Torso",
  "AnatomicalLocationType_LeftForeArm": "4 - Left Fore Arm",
  "AnatomicalLocationType_LeftThigh": "7 - Left Thigh",
  "AnatomicalLocationType_LeftTibia": "8 - Left Tibia",
  "AnatomicalLocationType_LeftUpperArm": "3 - Left Upper Arm",
  "AnatomicalLocationType_RightForeArm": "2 - Right Fore Arm",
  "AnatomicalLocationType_RightThigh": "5 - Right Thigh",
  "AnatomicalLocationType_RightTibia": "6 - Right Tibia",
  "AnatomicalLocationType_RightUpperArm": "1 - Right Upper Arm",
  "AnatomicalLocationType_UpperSpine": "Upper Spine",
  "ConditionType_New": "New",
  "ConditionType_Used": "Used",
  "Confirmed": "Confirmed",
  "ConfirmPassword": "Confirm password",
  "ConfirmPasswordValidateMessage": "The passwords do not match",
  "ConfirmToken": "Confirm token",
  "CreateNewAccount": "Create a new account",
  "Email": "Email",
  "EmailActivatedBody": "Your account has been activated",
  "EmailActivatedSubject": "Activated!",
  "EmailActivationUserSubject": "Welcome!",
  "EmailForgotPasswordSubject": "Forgot password",
  "EmailForgotUsernameSubject": "Username reminder",
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
  "PasswordSuccessufullySent": "We have sent you an email with a link to reset a password",
  "ResetPasswordMessage": "Use this form to reset your password.",
  "UsernameSuccessufullySent": "We have sent you an email with your username.",
  "EmailUsed": "Email already used",
  "UsernameUsed": "Username already used",
  "UserSignupMessage": "User has been created. We have sent a request to the administrator, please check back when your account has been approved",
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
  "WrongObjectAccess": "You don't have access to that",
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
  "EnterEquipment": "Enter the serial #, location or mac address of an equipment",
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
  "EnterComplexEquipment": "Enter a MAC address, a physical location or a serial #",
  "Admin": "Admin",
  "Required": "required",
  "Link": "Link",
  "None": "None",
  "Unlink": "Unlink",
  "Save": "Save",
  "SelectEquipment": "Select equipments",
  "Address": "Address",
  "Organization": "Organization",
  "Organizations": "Organizations",
  "Phone": "Phone",
  "EnterOrganization": "Enter the name, id or phone of organization",
  "UserIsExists": "User already exists",
  "EmailInviteAdminUserSubject": "Welcome to Heddoko!",
  "UserAlreadyInOrganizations": "User is already in another Organization",
  "InviteMessage": "Use this form to complete your invitation",
  "InviteToken": "Invite token",
  "UserSignupOrganizationMessage": "Your account has been updated. Your organization is now active",
  "Amount": "Amount",
  "ExpirationAt": "Expiration at",
  "License": "License",
  "LicenseUsed": "License already used",
  "LicenseAmountUsed": "Organization can't have less licenses than amount in use",
  "Type": "Type",
  "UserSignupUserNonLicenseOrganizationMessage": "Your account has been updated. Ask admin of your organization for license!",
  "UserSignupUserOrganizationMessage": "Your account has been updated. You can now use your account in the Heddoko app!",
  "LicenseType_DataAnalysis": "Data analysis",
  "LicenseType_DataCollection": "Data collection",
  "DataAnalysisAmount": "Data anaylysis amount",
  "DataCollectorAmount": "Data collectors amount",
  "OrganizationName": "Name of organization",
  "Used": "Used",
  "OrganizationNameUsed": "There is already an organization with that name.",
  "Licenses": "Licenses",
  "User": "User",
  "Users": "Users",
  "EnterLicense": "Enter the id of license",
  "EnterUser": "Enter the firstname, lastname or email of user",
  "Role": "Role",
  "EmailInviteUserSubject": "Welcome to Heddoko!",
  "EmptyLicense": "No license",
  "Profile": "Profile",
  "OldPassword": "Old password",
  "ProfileSaveMessage": "Your profile has been updated",
  "WrongLicenseActive": "License is not active",
  "WrongLicenseDeleted": "License is removed",
  "WrongLicenseExpiration": "License is expired",
  "WrongExpirationAtDate": "Expiration date cannot be set to the past!",
  "Restore": "Restore",
  "ShowDeleted": "Show deleted",
  "WrongLicenseAdmin": "Admin can't also be a license admin",
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
  "SupportSent": "Thank you! We have sent an email with your request to our support team.",
  "WrongAttachmentSize": "Total size can't be more than 20 mb.",
  "All": "All",
  "ExpiredSoon": "License is almost expired",
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
  "EquipmentStatusType_ForWash": "For wash",
  "EquipmentStatusType_Trash": "Trash",
  "SensorType_EMIMU": "EM IMU",
  "SensorType_NodIMU": "Nod IMU",
  "SensorType_StretchSense": "Stretch Sense",
  "EquipmentQAStatusType_Fail": "Fail",
  "EquipmentQAStatusType_TestedAndReady": "Tested and Ready",
  "KitQAStatusType_Fail": "Fail",
  "KitQAStatusType_TestedAndReady": "Tested and Ready",
  "SensorSetQAStatusType_Fail": "Fail",
  "SensorSetQAStatusType_TestedAndReady": "Tested and Ready",
  "SensorQAStatusType_FirmwareUpdated": "Firmware Updated",
  "SensorQAStatusType_SeatingInBase": "Sensor seating in base",
  "SensorQAStatusType_LED": "Sensor LED",
  "SensorQAStatusType_Orientation": "Sensor Orientation",
  "SensorQAStatusType_Drift": "Sensor Drift",
  "SensorQAStatusType_TestedAndReady": "Tested and Ready",
  "ShirtQAStatusType_BaseplateInspection": "Baseplates inspection",
  "ShirtQAStatusType_ConnectorInspection": "Connector inspection",
  "ShirtQAStatusType_Fail": "Fail",
  "ShirtQAStatusType_HeatShrinkInspection": "Heat Shrink Inspection",
  "ShirtQAStatusType_IDLabelInspection": "ID label inspection",
  "ShirtQAStatusType_PowerInspection": "Power inspection",
  "ShirtQAStatusType_SeamsInspection": "Seams inspection",
  "ShirtQAStatusType_TestedAndReady": "Tested and Ready",
  "ShirtQAStatusType_WiringInspection": "Wiring inspection",
  "ShirtOctopiQAStatusType_BaseplateInspection": "Baseplates inspection",
  "ShirtOctopiQAStatusType_ConnectorInspection": "Connector inspection",
  "ShirtOctopiQAStatusType_Fail": "Fail",
  "ShirtOctopiQAStatusType_HeatShrinkInspection": "Heat Shrink Inspection",
  "ShirtOctopiQAStatusType_IDLabelInspection": "ID label inspection",
  "ShirtOctopiQAStatusType_PowerInspection": "Power inspection",
  "ShirtOctopiQAStatusType_SeamsInspection": "Seams inspection",
  "ShirtOctopiQAStatusType_TestedAndReady": "Tested and Ready",
  "ShirtOctopiQAStatusType_WiringInspection": "Wiring inspection",
  "PantsQAStatusType_BaseplateInspection": "Baseplates inspection",
  "PantsQAStatusType_ConnectorInspection": "Connector inspection",
  "PantsQAStatusType_Fail": "Fail",
  "PantsQAStatusType_HeatShrinkInspection": "Heat Shrink Inspection",
  "PantsQAStatusType_IDLabelInspection": "ID label inspection",
  "PantsQAStatusType_PowerInspection": "Power inspection",
  "PantsQAStatusType_SeamsInspection": "Seams inspection",
  "PantsQAStatusType_TestedAndReady": "Tested and Ready",
  "PantsQAStatusType_WiringInspection": "Wiring inspection",
  "PantsOctopiQAStatusType_BaseplateInspection": "Baseplates inspection",
  "PantsOctopiQAStatusType_ConnectorInspection": "Connector inspection",
  "PantsOctopiQAStatusType_Fail": "Fail",
  "PantsOctopiQAStatusType_HeatShrinkInspection": "Heat Shrink Inspection",
  "PantsOctopiQAStatusType_IDLabelInspection": "ID label inspection",
  "PantsOctopiQAStatusType_PowerInspection": "Power inspection",
  "PantsOctopiQAStatusType_SeamsInspection": "Seams inspection",
  "PantsOctopiQAStatusType_TestedAndReady": "Tested and Ready",
  "PantsOctopiQAStatusType_WiringInspection": "Wiring inspection",
  "BrainpackQAStatusType_AppConnectsToBrainpack": "App connects to to brainpack",
  "BrainpackQAStatusType_AppDisconnectsFromBrainpack": "App disconnects from brainpack",
  "BrainpackQAStatusType_AppLocatesBrainpack": "App locates brainpack",
  "BrainpackQAStatusType_AppPairsBrainpack": "App pairs brainpack",
  "BrainpackQAStatusType_Buttons1mmOfRecess": "Buttons 1 mm of recess",
  "BrainpackQAStatusType_ButtonsReturnToInitialDepth": "Buttons return to initial depth",
  "BrainpackQAStatusType_ChargingIndicatorLed": "Charging indicator LED",
  "BrainpackQAStatusType_Fail": "Fail",
  "BrainpackQAStatusType_FullChargingCycle": "Full Charging Cycle",
  "BrainpackQAStatusType_LedColors": "LED colors Fail",
  "BrainpackQAStatusType_PairingWithIMUs": "Pairing with IMUs",
  "BrainpackQAStatusType_PowerButtonWorking": "Power button working",
  "BrainpackQAStatusType_RecordButtonWorking": "Record button working",
  "BrainpackQAStatusType_RecordingFilenameSaved": "Recording File name saved",
  "BrainpackQAStatusType_RecoveryFromShutdown": "Recovery from shutdown",
  "BrainpackQAStatusType_ResetButtonWorking": "Reset button working",
  "BrainpackQAStatusType_SDCardIsElectronicallyLabeledWithBatteryPackId": "SD card is electronically labeled with battery pack ID",
  "BrainpackQAStatusType_SetRecordingName": "Set recording name",
  "BrainpackQAStatusType_SettingsFileUpdated": "Settings file updated",
  "BrainpackQAStatusType_StateChangeIdleToRecording": "State change: idle to recording",
  "BrainpackQAStatusType_StateChangeIdleToReset": "State change: idle to reset",
  "BrainpackQAStatusType_StateChangeRecordingToError": "State change: recording to error",
  "BrainpackQAStatusType_StateChangeRecordingToIdle": "State change: recording to idle",
  "BrainpackQAStatusType_StateChangeRecordingToReset": "State change: recording to reset",
  "BrainpackQAStatusType_StateIndicatorLed": "State indicator LED",
  "BrainpackQAStatusType_TestedAndReady": "Tested and Ready",
  "PowerboardQAStatusType_PowerboardProgrammed": "Powerboard Programmed",
  "PowerboardQAStatusType_PowerboardUSBEnum": "Powerboard USB Enumerates",
  "PowerboardQAStatusType_BatteryInstalled": "Battery Installed",
  "PowerboardQAStatusType_TestedAndReady": "Tested and Ready",
  "DataboardQAStatusType_BootloaderProgrammed": "Bootloader Programmed",
  "DataboardQAStatusType_SDCardAssigned": "SD Card Assigned",
  "DataboardQAStatusType_BrainMCUProgrammed": "Brain MCU Programmed",
  "DataboardQAStatusType_Quintic1Programmed": "Quintic-1 Programmed",
  "DataboardQAStatusType_Quintic2Programmed": "Quintic-2 Programmed",
  "DataboardQAStatusType_Quintic3Programmed": "Quintic-3 Programmed",
  "DataboardQAStatusType_SerialNumberProgrammed": "Serial Number Programmed",
  "DataboardQAStatusType_BluetoothConnectionTested": "Bluetooth connection tested",
  "DataboardQAStatusType_PowerButtonVerified": "Power Button Verified",
  "DataboardQAStatusType_ResetButtonVerified": "Reset Button Verified",
  "DataboardQAStatusType_RecordButtonVerified": "Record Button Verified",
  "DataboardQAStatusType_StreamingFrameTest": "Streaming/Recording Frame test with 9 Nods",
  "DataboardQAStatusType_FullChargeCycleComplete": "Full Charge Cycle Completed",
  "DataboardQAStatusType_TestedAndReady": "Tested and Ready",
  "SizeType_ExtraLarge": "Extra Large",
  "SizeType_Large": "Large",
  "SizeType_Medium": "Medium",
  "SizeType_Small": "Small",
  "Change": "Change",
  "InvalidPhone": "Phone number entered is not valid",
  "QAChecklist": "QA Checklist",
  "FilterStatus": "Filter by Status",
  "Filter": "Filter",
  "Software": "Software",
  "WrongOldPassword": "Old password is wrong",
  "Team": "Team",
  "Teams": "Teams",
  "History": "History",
  "Date": "Date",
  "LicenseIsNotReady": "You don't seem to have a Heddoko License assigned to you. Please notify organization admin.",
  "Guide": "Guide",
  "Requirements": "Requirements",
  "UserIsNotApproved": "User is not approved",
  "Approve": "Approve",
  "CantChangeInvite": "You can't change invite status",
  "CantChangePending": "You can't change pending status",
  "CantSetInvite": "You can't set invite status",
  "CantSetPending": "You can't set pending status",
  "WrongLockedOut": "You have been locked out.",
  "RequiresVerification": "RequiresVerification",
  "WrongConfirm": "Email is not confirmed. <a href=\"{0}\">Resend</a> activation email for user {1}",
  "UserDoesntExist": "User doesn't exist",
  "Analyst": "Analyst",
  "LicenseAdmin": "License Admin",
  "Worker": "Worker",
  "EmailHasBeenSent": "Email has been sent",
  "ActivationEmailHasBeenSentCheckEmail": "Activation email has been successfully sent, please check your email",
  "ResendActivation": "Resend activation",
  "UserIsNotInvited": "User should be invited",
  "UserDoesntHaveKit": "User should have a kit",
  "UserIsNotInTeam": "User should be in a team",
  "WrongTeam": "User is in the wrong team",
  "LicenseIsExpiring": "License is expiring",
  "StreamChannelOpened": "Channel for receiving stream was opened for your team",
  "StreamChannelClosed": "Channel for receiving stream was closed for your team",
  "LicenseAddedToOrganization": "License {0} has been added to your organization",
  "LicenseExpired": "License {0} is expired",
  "LicenseExpiring": "License {0} is expiring at {1}",
  "LicenseRemovedFromOrganization": "License {0} has been removed from your organization",
  "LicenseAddedToUser": "License {0} has been assigned to you",
  "LicenseChangedForUser": "Your license has been changed to {0}",
  "LicenseRemovedFromUser": "License {0} has been unassigned from you",
  "Ergoscore": "Ergoscore",
  "LicenseRemovedFromUser": "License {0} has been unassigned from you",
  "IsInvalidMessage": "{0} is invalid",
  "WrongFilesCount": "The number of files must be greater than or equal to {0} and less than or equal to {1}",
  "AssetType_AnalysisFrameData": "Analysis frame data",
  "AssetType_ProcessedFrameData": "Processed frame data",
  "AssetType_RawFrameData": "Raw frame data",
  "FirmwareType_DefaultRecords": "Default records",
  "AssetType": "Asset Type"
};
	return $.extend({}, i18n.Resources || {}, strings);
}());