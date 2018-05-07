namespace ImageService.Communication
{
    public delegate string execute(int commandID, string[] args, out bool resultSuccesful);
}