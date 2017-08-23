[
  {
    "name": "Rich text editor",
    "alias": "rte",
    "view": "rte",
    "icon": "icon-article"
  },
  {
    "name": "Image",
    "alias": "media",
    "view": "media",
    "icon": "icon-picture"
  },
  {
    "name": "Macro",
    "alias": "macro",
    "view": "macro",
    "icon": "icon-settings-alt"
  },
  {
    "name": "Embed",
    "alias": "embed",
    "view": "embed",
    "icon": "icon-movie-alt"
  },
  {
    "name": "Headline",
    "alias": "headline",
    "view": "textstring",
    "icon": "icon-coin",
    "config": {
      "style": "font-size: 36px; line-height: 45px; font-weight: bold",
      "markup": "<h1>#value#</h1>"
    }
  },
  {
    "name": "Quote",
    "alias": "quote",
    "view": "textstring",
    "icon": "icon-quote",
    "config": {
      "style": "border-left: 3px solid #ccc; padding: 10px; color: #ccc; font-family: serif; font-style: italic; font-size: 18px",
      "markup": "<blockquote>#value#</blockquote>"
    }
  },
  {
    "name": "Form Picker",
    "alias": "manaoFormPicker",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-autofill color-blue",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Select a form",
          "alias": "form",
          "propretyType": {},
          "dataType": "bba2d1c8-79d7-4236-969f-a9650257f125"
        },
        {
          "name": "Form header",
          "alias": "formContent",
          "propretyType": {},
          "dataType": "706bdefc-7cff-4c9d-846e-d69eda6c3a00"
        }
      ],
      "renderInGrid": "1",
      "min": 1,
      "max": 1,
      "frontView": ""
    }
  },
  {
    "name": "Text",
    "alias": "manaoTextField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-textfield color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Text-Area",
    "alias": "manaoTextAreaField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-rawaccesslogs color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Label",
    "alias": "manaoLabelField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-font color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Dropdown",
    "alias": "manaoDropdownField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-dropmenu color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "frontView": "",
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Prevalues",
          "alias": "prevalues",
          "propretyType": {},
          "dataType": "3a7075b0-0b2b-4fdb-8c23-bd558a544245"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1"
    }
  },
  {
    "name": "Checkbox",
    "alias": "manaoCheckboxField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-checkbox color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Prevalues",
          "alias": "prevalues",
          "propretyType": {},
          "dataType": "3a7075b0-0b2b-4fdb-8c23-bd558a544245"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Radio",
    "alias": "manaoRadioField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-radiobutton color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Prevalues",
          "alias": "prevalues",
          "propretyType": {},
          "dataType": "3a7075b0-0b2b-4fdb-8c23-bd558a544245"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Phone",
    "alias": "manaoPhoneField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-phone color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Error message for invalid format",
          "alias": "messageInvalidFormat",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Email",
    "alias": "manaoEmailField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-emailalt color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Error message for invalid format",
          "alias": "messageInvalidFormat",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Upload",
    "alias": "manaoUploadField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-uploadalt color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Select file text",
          "alias": "selectFileText",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        },
        {
          "name": "Change file text",
          "alias": "changeFileText",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        },
        {
          "name": "Upload Folder",
          "alias": "uploadFolder",
          "propretyType": {},
          "dataType": "131fa411-b256-448f-a77d-5ff0c03dfef6",
          "description": "Select folder for file uploaded"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Error message for invalid format",
          "alias": "messageForInvalidFormat",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        },
        {
          "name": "Error message for over max file size",
          "alias": "messageForOverMaxFileSize",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  },
  {
    "name": "Drag Drop Upload",
    "alias": "manaoDragDropUploadField",
    "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
    "icon": "icon-whhg-cloudaltupload color-orange",
    "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
    "config": {
      "editors": [
        {
          "name": "Name",
          "alias": "name",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Placeholder",
          "alias": "placeholder",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Select file text",
          "alias": "selectFileText",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        },
        {
          "name": "Upload Folder",
          "alias": "uploadFolder",
          "propretyType": {},
          "dataType": "131fa411-b256-448f-a77d-5ff0c03dfef6",
          "description": "Select folder for file uploaded"
        },
        {
          "name": "Mandatory",
          "alias": "mandatory",
          "propretyType": {},
          "dataType": "76ce1e1f-48a2-4f2b-948f-6efcf7f41aa9"
        },
        {
          "name": "Error message for mandatory",
          "alias": "messageMandatory",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
        },
        {
          "name": "Error message for invalid format",
          "alias": "messageForInvalidFormat",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        },
        {
          "name": "Error message for over max file size",
          "alias": "messageForOverMaxFileSize",
          "propretyType": {},
          "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae",
          "description": ""
        }
      ],
      "renderInGrid": "1",
      "frontView": ""
    }
  }
]