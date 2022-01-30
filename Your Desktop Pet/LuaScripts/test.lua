windows = GetWindows(false, false)

function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
end

max = tablelength(windows)

for i = 0, max - 1, 1 do
    print(windows[i].name)
    print(windows[i].bounds)
    print("\n")
end

print(GetDesktopBounds())
SaveValue("Testening_value", windows[0].name)
print(ReadValue("Testening_value"))
print(GetMousePos())