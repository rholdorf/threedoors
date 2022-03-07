var rnd = new Random();
var numberOfDoors = 3;
var cummulative = 0;
var times = 100000;
var shouldSwitch = true;

for(int i = 0; i <= times; i++)
{
    var pickedDoors = new bool[numberOfDoors];
    var correctDoor = (int)rnd.NextInt64(numberOfDoors); // escolhe uma porta como a premiada
    var audiencePick = (int)rnd.NextInt64(numberOfDoors); // escolhe uma porta qualquer
    pickedDoors[audiencePick]=true; // marca uma das portas como escolhida

    // apresentador escolhe uma porta, que deve ser diferente da escolhida
    int presenterPick = 0;
    while(true)
    {
        presenterPick = (int)rnd.NextInt64(numberOfDoors);
        if(presenterPick == correctDoor) // mesma porta que a correta, não pode ser escolhida pelo apresentador
            continue;

        if(presenterPick == audiencePick) // mesma porta que a escolhida, não pode ser escolhida pelo apresentador
            continue;

        break; // só pode ser uma porta diferente que a correta e diferente que a escolhida
    }
    
    pickedDoors[presenterPick] = true; // marca a porta escolhida pelo apresentador (que é aberta para a audiencia, mostrando que não tem nada dentro)

    if(shouldSwitch)
    {
        // audiencia troca de porta, pela outra que não é a que ela tinha escolhida nem a que o apresentador abriu
        int otherDoor = -1;
        for(int j = 0; j < numberOfDoors; j++)
        {
            if(pickedDoors[j])
                continue; // ou é a porta originalmente escolhida, ou é a porta aberta pelo apresentador

            otherDoor = j; // é a outra porta que não tinha sido escolhida nem aberta
            break;
        }

        // sanity check
        if(otherDoor == audiencePick 
        || otherDoor == presenterPick
        || otherDoor == -1)
            throw new InvalidDataException("impossible to switch doors");

        audiencePick = otherDoor; // troca a escolha da audiência
    }   

    if(audiencePick == correctDoor)
        cummulative++; // porta certa
}

Console.WriteLine(((double)cummulative/(double)times)* 100D); // percentual de vezes que acertou a porta
