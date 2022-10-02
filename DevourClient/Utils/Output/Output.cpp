#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include "Output.hpp"
#include <fstream>
#include <iostream>

#define TO_FILE
constexpr auto LOGS_FILENAME = "DevourClient-dev.log";

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
#ifdef TO_FILE
    std::remove(LOGS_FILENAME);
#endif

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

    va_list args;
    va_start(args, fmt);

#ifdef TO_FILE
    FILE* f;
    fopen_s(&f, LOGS_FILENAME, "a");
    if (f)
    {
        vfprintf(f, fmt, args);
        fclose(f);
    }
#endif

#if _DEBUG
    vprintf(fmt, args);
#endif

    va_end(args);

#if _DEBUG
    return true;
#endif

    return false;
}