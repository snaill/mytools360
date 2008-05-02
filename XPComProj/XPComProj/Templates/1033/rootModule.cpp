#include "stdafx.h"
#include "[!output PROJECT_NAME]Impl.h"
#include "nsIGenericFactory.h"

///////////////////////////////////////////////
NS_GENERIC_FACTORY_CONSTRUCTOR(C[!output PROJECT_NAME])

static nsModuleComponentInfo components[] =
{
    {
       [!output PROJECT_NAME_UPPER]_CLASSNAME, 
       [!output PROJECT_NAME_UPPER]_CID,
       [!output PROJECT_NAME_UPPER]_CONTRACTID,
       C[!output PROJECT_NAME]Constructor,
    }
};

NS_IMPL_NSGETMODULE("[!output PROJECT_NAME]Module", components) 

