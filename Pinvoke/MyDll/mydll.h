#ifdef MYDLL_EXPORTS
#define MYDLL_API _declspec(dllexport)
#else
#define MYDLL_API _declspec(dllimport)
#endif

extern "C" {
	 typedef void(__stdcall* MyIntCallback)(int);

	 typedef void(__stdcall* MyStringCallback)(char *);

	 MYDLL_API void RegisterIntCallback(MyIntCallback callback);

	 MYDLL_API void CallIntCallback(int value);

	 MYDLL_API void RegisterStringCallback(MyStringCallback callback);

	 MYDLL_API void CallStringCallback(char *data);
}