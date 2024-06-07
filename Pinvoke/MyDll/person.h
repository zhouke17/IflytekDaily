#pragma once
extern "C"
{
    class Person
    {
    public:
        char* username;
        char* password;
    };

    _declspec(dllexport) char* AddPerson(Person person);
}

