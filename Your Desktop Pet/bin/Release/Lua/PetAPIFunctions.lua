function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
end

function isColliding(a, b)
    if ((b.X >= a.X + a.Width) or (b.X + b.Width <= a.X) or (b.Y >= a.Y + a.Height) or (b.Y + b.Height <= a.Y)) then
        return false 
    else 
        return true
    end
 end

function BoundsFromXYWH(x, y, w, h)
    t = {}
    t.X = x
    t.Y = y
    t.Width = w
    t.Height = h
    return t
end