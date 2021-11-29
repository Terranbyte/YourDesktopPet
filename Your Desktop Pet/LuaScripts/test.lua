windows = _GetWindows(false, false)

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

print(_GetDesktopBounds())
_SaveValue("Testening_value", windows[0].name)
print(_ReadValue("Testening_value"))
print(_GetMousePos())