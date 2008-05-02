#pragma once

#include "[!output PROJECT_NAME].h"

#define [!output PROJECT_NAME_UPPER]_CONTRACTID 	"@[!output WEBSITE_NAME]/[!output PROJECT_NAME];1"
#define [!output PROJECT_NAME_UPPER]_CLASSNAME		"[!output PROJECT_NAME]"
#define [!output PROJECT_NAME_UPPER]_CID		[!output CLASS_REGISTRY_FORMAT]

class C[!output PROJECT_NAME] : public I[!output PROJECT_NAME]
{
public:
	NS_DECL_ISUPPORTS
	NS_DECL_I[!output PROJECT_NAME_UPPER]

	C[!output PROJECT_NAME](void);
	~C[!output PROJECT_NAME](void);


};
