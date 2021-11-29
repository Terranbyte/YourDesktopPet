function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
end

function isColliding(a, b)
    if ((b.x >= a.x + a.w) or (b.x + b.w <= a.x) or (b.y >= a.y + a.h) or (b.y + b.h <= a.y)) then
        return false 
    else 
        return true
    end
 end

function BoundsFromXYWH(x, y, w, h)
    t = {}
    t.x = x
    t.y = y
    t.w = w
    t.h = h
    return t
end