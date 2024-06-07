#include "mydll.h"

static MyIntCallback myCallback;
static MyStringCallback myStringCallback;

void RegisterIntCallback(MyIntCallback callback)
{
    myCallback = callback;
}

void CallIntCallback(int value)
{
    myCallback(value);
}

void RegisterStringCallback(MyStringCallback callback)
{
    myStringCallback = callback;
}

void CallStringCallback(char *data)
{
    myStringCallback(data);
}



