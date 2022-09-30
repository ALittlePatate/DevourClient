#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include "Output.hpp"
#include <iostream>

#define LOG

#if _DEBUG //We'll need fp to write into the console, using it on debug builds only
FILE* fp;
#endif

void CloseConsole() {
#if _DEBUG
    fclose(fp);
    FreeConsole();
#endif
}

bool OpenConsole() {
    /*
    If we're running in debug mode, we open the output console and return true
    Else we return false
    The booleans are used for the debug_mode variable, this can be usefull probably
    */
#if _DEBUG
    AllocConsole();
    freopen_s(&fp, "CONOUT$", "w", stdout); // output only
    return true;
#endif
    return false;
}

bool print(const char* fmt, ...) {
    /*
    Just a wrapper for std::cout, this skips the whole if _DEBUG thing
    */
#if _DEBUG
    va_list args;
    va_start(args, fmt);
    vprintf(fmt, args);
    va_end(args);
    return true;
#endif

    return false;
}